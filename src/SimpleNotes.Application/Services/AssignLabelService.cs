using FluentResults;
using SimpleNotes.Application.Abstractions;
using SimpleNotes.Application.Errors;

namespace SimpleNotes.Application.Services;

public class AssignLabelService(INoteLabelsRepository noteLabelsRepository) : IAssignLabelService
{
    public async Task<Result> AssignLabelAsync(Guid noteId, Guid labelId, CancellationToken cancellationToken = default)
    {
        var noteLabels = await noteLabelsRepository.GetByIdAsync(noteId, cancellationToken);
        if (noteLabels is null)
        {
            return new NoteNotFoundError(noteId);
        }

        var assigned = noteLabels.AssignLabel(labelId);

        if (assigned)
        {
            await noteLabelsRepository.UpdateLabelsAsync(noteLabels, cancellationToken);
        }

        return Result.Ok();
    }
}