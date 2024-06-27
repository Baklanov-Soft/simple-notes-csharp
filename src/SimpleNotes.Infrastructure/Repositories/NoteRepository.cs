using Microsoft.EntityFrameworkCore;
using SimpleNotes.Application.Abstractions;
using SimpleNotes.Domain;
using SimpleNotes.Infrastructure.DbContexts;

namespace SimpleNotes.Infrastructure.Repositories;

public class NoteRepository(INotesDbContext dbContext) : INoteRepository
{
    public async Task<Note?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var note = await dbContext.Notes
            .Where(n => n.Id == id)
            .Select(n => new Note(n.Id, n.Name, 250,
                n.TreeNodeLabels!.Select(l => new Label(l.Label.Id, l.Label.Name, l.Label.Color)).ToHashSet())
            {
                Text = n.Text
            })
            .FirstOrDefaultAsync(cancellationToken);

        return note;
    }
}