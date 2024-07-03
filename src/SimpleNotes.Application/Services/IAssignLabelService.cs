using FluentResults;

namespace SimpleNotes.Application.Services;

public interface IAssignLabelService
{
    Task<Result> AssignLabelAsync(Guid noteId, Guid labelId, CancellationToken cancellationToken = default);
}