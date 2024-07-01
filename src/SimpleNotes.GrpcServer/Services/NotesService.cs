using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using SimpleNotes.Application.Abstractions;
using SimpleNotes.Application.Models;

namespace SimpleNotes.GrpcServer.Services;

using NotesServiceBase = SimpleNotes.GrpcServer.NotesService.NotesServiceBase;

public class NotesService(ICreateNoteService createNoteService, INoteRepository noteRepository) : NotesServiceBase
{
    public override async Task<Empty> CreateNote(CreateNoteRequest request, ServerCallContext context)
    {
        var createNoteDto = new CreateNoteDto
        {
            Name = request.Name,
            Text = request.Text,
            LabelIds = request.LabelIds?.LabelIds_
                .Select(labelId => Guid.ParseExact(labelId.Value, "D"))
                .ToHashSet()
        };

        if (request.ParentId is not null)
        {
            var parentId = Guid.ParseExact(request.ParentId.Value, "D");
            await createNoteService.CreateAsync(createNoteDto, parentId, context.CancellationToken);
        }
        else
        {
            await createNoteService.CreateAsync(createNoteDto, null, context.CancellationToken);
        }

        return new Empty();
    }

    public override async Task<Empty> DeleteNote(DeleteNoteRequest request, ServerCallContext context)
    {
        var noteId = Guid.ParseExact(request.NoteId.Value, "D");
        await noteRepository.DeleteAsync(noteId, context.CancellationToken);

        return new Empty();
    }
}