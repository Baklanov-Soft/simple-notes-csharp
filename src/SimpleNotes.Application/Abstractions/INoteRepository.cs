using FluentResults;
using SimpleNotes.Domain;

namespace SimpleNotes.Application.Abstractions;

public interface INoteRepository
{
    Task<Note?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<string>> GetPathAsync(Guid id, CancellationToken cancellationToken = default);
}