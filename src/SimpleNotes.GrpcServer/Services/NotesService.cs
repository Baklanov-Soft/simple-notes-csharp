using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using SimpleNotes.Application.Abstractions;
using SimpleNotes.Application.Models;
using SimpleNotes.Application.Services;

namespace SimpleNotes.GrpcServer.Services;

using NotesServiceBase = SimpleNotes.GrpcServer.NotesService.NotesServiceBase;

public class NotesService(
    ICreateNoteService createNoteService,
    INoteRepository noteRepository,
    IAssignLabelService assignLabelService) : NotesServiceBase
{
    public override async Task<NoteCreatedResponse> CreateNote(CreateNoteRequest request, ServerCallContext context)
    {
        var createNoteDto = new CreateNoteDto
        {
            Name = request.Name,
            Text = request.Text,
            LabelIds = request.LabelIds?.LabelIds_
                .Select(labelId => Guid.ParseExact(labelId.Value, "D"))
                .ToHashSet()
        };

        ReadNoteDto readNoteDto;
        if (request.ParentId is not null)
        {
            var parentId = Guid.ParseExact(request.ParentId.Value, "D");
            readNoteDto = await createNoteService.CreateAsync(createNoteDto, parentId, context.CancellationToken);
        }
        else
        {
            readNoteDto = await createNoteService.CreateAsync(createNoteDto, null, context.CancellationToken);
        }

        return new NoteCreatedResponse
        {
            Id = new UUID
            {
                Value = readNoteDto.Id.ToString()
            },
            Name = readNoteDto.Name,
            Text = readNoteDto.Text
        };
    }

    public override async Task<Empty> DeleteNote(DeleteNoteRequest request, ServerCallContext context)
    {
        var noteId = Guid.ParseExact(request.NoteId.Value, "D");
        await noteRepository.DeleteAsync(noteId, context.CancellationToken);

        return new Empty();
    }

    public override async Task<Empty> AssignLabel(AssignLabelRequest request, ServerCallContext context)
    {
        var noteId = Guid.ParseExact(request.NoteId.Value, "D");
        var labelId = Guid.ParseExact(request.LabelId.Value, "D");

        await assignLabelService.AssignLabelAsync(noteId, labelId, context.CancellationToken);

        return new Empty();
    }
}