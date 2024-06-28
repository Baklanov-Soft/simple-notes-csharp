using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using SimpleNotes.Application.Abstractions;
using SimpleNotes.Application.Models;

namespace SimpleNotes.GrpcServer.Services;

using NotesServiceBase = SimpleNotes.GrpcServer.NotesService.NotesServiceBase;

public class NotesService(ICreateNoteService createNoteService) : NotesServiceBase
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
        
        var parentId= Guid.ParseExact(request.ParentId.Value, "D");
        await createNoteService.CreateAsync(createNoteDto, parentId, context.CancellationToken);

        return new Empty();
    }
}