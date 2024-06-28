using Microsoft.EntityFrameworkCore;
using SimpleNotes.Application.Abstractions;
using SimpleNotes.Application.Models;
using SimpleNotes.Infrastructure.DbContexts;

namespace SimpleNotes.Infrastructure.Services;

public class CreateNoteService(INotesDbContext dbContext, INoteFactory noteFactory) : ICreateNoteService
{
    public async Task CreateAsync(CreateNoteDto createNoteDto, Guid parentId,
        CancellationToken cancellationToken = default)
    {
        var parentPath = await dbContext.TreeNodes
            .AsNoTracking()
            .Where(node => node.Id == parentId)
            .Select(node => node.Path.ToString())
            .FirstOrDefaultAsync(cancellationToken);

        var note = noteFactory.Create(createNoteDto, parentPath);
        dbContext.Notes.Add(note);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}