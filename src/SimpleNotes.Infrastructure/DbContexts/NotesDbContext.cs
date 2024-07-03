using Microsoft.EntityFrameworkCore;
using SimpleNotes.Infrastructure.Entities;
using SimpleNotes.Infrastructure.Entities.Configurations;
using SimpleNotes.Infrastructure.Enums;

namespace SimpleNotes.Infrastructure.DbContexts;

public class NotesDbContext(DbContextOptions options) : DbContext(options), INotesDbContext
{
    public DbSet<TreeNode> TreeNodes => Set<TreeNode>();
    public DbSet<Note> Notes => Set<Note>();
    public DbSet<Label> Labels => Set<Label>();
    public DbSet<TreeNodeLabel> TreeNodeLabels => Set<TreeNodeLabel>();

    public void Migrate() => Database.Migrate();
    public bool EnsureCreated() => Database.EnsureCreated();

    public Task MigrateAsync(CancellationToken cancellationToken = default) =>
        Database.MigrateAsync(cancellationToken);

    public Task EnsureCreatedAsync(CancellationToken cancellationToken = default) =>
        Database.EnsureCreatedAsync(cancellationToken);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<NodeType>();
        modelBuilder.ApplyConfiguration(new TreeNodeConfiguration());
        modelBuilder.ApplyConfiguration(new NoteConfiguration());
        modelBuilder.ApplyConfiguration(new LabelConfiguration());
        modelBuilder.ApplyConfiguration(new TreeNodeLabelConfiguration());
    }
}