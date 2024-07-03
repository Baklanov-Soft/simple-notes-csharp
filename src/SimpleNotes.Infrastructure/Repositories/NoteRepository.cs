using FluentResults;
using Microsoft.EntityFrameworkCore;
using SimpleNotes.Application.Abstractions;
using SimpleNotes.Application.Errors;
using SimpleNotes.Infrastructure.DbContexts;
using SimpleNotes.Infrastructure.Entities;
using SimpleNotes.Infrastructure.Enums;
using Note = SimpleNotes.Domain.Note;

namespace SimpleNotes.Infrastructure.Repositories;

public class NoteRepository(INotesDbContext dbContext) : INoteRepository
{
    public async Task<Note?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var note = await dbContext.Notes
            .AsNoTracking()
            .Where(n => n.Id == id)
            .Select(n => new Note(n.Id, n.Name, 250)
            {
                Text = n.Text
            })
            .FirstOrDefaultAsync(cancellationToken);

        return note;
    }

    public async Task<Result<string>> GetFolderPathAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var path = await dbContext.TreeNodes
            .AsNoTracking()
            .Where(node => node.Type == NodeType.Folder)
            .Where(node => node.Id == id)
            .Select(node => node.Path.ToString())
            .FirstOrDefaultAsync(cancellationToken);

        if (path is not null)
        {
            return path;
        }

        return new ParentNotFoundError(id);
    }

    public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var treeNode = new TreeNode
        {
            Id = id
        };
        dbContext.TreeNodes.Remove(treeNode);
        return dbContext.SaveChangesAsync(cancellationToken);
    }
}