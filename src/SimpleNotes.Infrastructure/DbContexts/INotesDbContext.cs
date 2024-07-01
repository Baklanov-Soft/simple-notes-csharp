using Microsoft.EntityFrameworkCore;
using SimpleNotes.Infrastructure.Entities;

namespace SimpleNotes.Infrastructure.DbContexts;

public interface INotesDbContext : IDisposable, IAsyncDisposable
{
    DbSet<TreeNode> TreeNodes { get; }
    DbSet<Note> Notes { get; }
    DbSet<Label> Labels { get; }
    DbSet<TreeNodeLabel> TreeNodeLabels { get; }

    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    void Migrate();
    Task MigrateAsync(CancellationToken cancellationToken = default);
    bool EnsureCreated();
    Task EnsureCreatedAsync(CancellationToken cancellationToken = default);
}