using SimpleNotes.Domain;

namespace SimpleNotes.Application.Abstractions;

public interface INoteLabelsRepository
{
    Task<NoteLabels?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task UpdateLabelsAsync(NoteLabels noteLabels, CancellationToken cancellationToken = default);
}