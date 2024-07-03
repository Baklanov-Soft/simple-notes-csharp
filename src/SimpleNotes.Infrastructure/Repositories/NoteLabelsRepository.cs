using Microsoft.EntityFrameworkCore;
using SimpleNotes.Application.Abstractions;
using SimpleNotes.Domain;
using SimpleNotes.Infrastructure.DbContexts;
using SimpleNotes.Infrastructure.Entities;

namespace SimpleNotes.Infrastructure.Repositories;

public class NoteLabelsRepository(INotesDbContext dbContext) : INoteLabelsRepository
{
    public async Task<NoteLabels?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var note = await dbContext.Notes
            .AsNoTracking()
            .Where(note => note.Id == id)
            .Select(note => new NoteLabels(note.Id, note.TreeNodeLabels!.Select(l => l.LabelId).ToHashSet()))
            .FirstOrDefaultAsync(cancellationToken);

        return note;
    }

    public async Task UpdateLabelsAsync(NoteLabels noteLabels, CancellationToken cancellationToken = default)
    {
        foreach (var newLabel in noteLabels.NewLabelsIterator())
        {
            dbContext.TreeNodeLabels.Add(new TreeNodeLabel
            {
                TreeNodeId = noteLabels.Id,
                LabelId = newLabel
            });
        }
        
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}