using SimpleNotes.Application.Models;

namespace SimpleNotes.Application.Abstractions;

public interface ICreateNoteService
{
    Task CreateAsync(CreateNoteDto createNoteDto, Guid parentId, CancellationToken cancellationToken = default);
}