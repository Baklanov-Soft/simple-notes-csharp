using Microsoft.EntityFrameworkCore;
using SimpleNotes.Infrastructure.Entities;

namespace SimpleNotes.Infrastructure.DbContexts;

public interface INotesDbContext
{
    DbSet<TreeNode> TreeNodes { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}