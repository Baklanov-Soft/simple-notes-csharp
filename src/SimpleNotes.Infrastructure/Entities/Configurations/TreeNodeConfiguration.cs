using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SimpleNotes.Infrastructure.Entities.Configurations;

public class TreeNodeConfiguration : IEntityTypeConfiguration<TreeNode>
{
    public void Configure(EntityTypeBuilder<TreeNode> builder)
    {
        builder.HasKey(n => n.Id);
        builder.Property(n => n.Name).HasMaxLength(250);
        builder.HasIndex(n => n.Path).HasMethod("GIST");
        builder.UseTptMappingStrategy();

        builder.HasMany(n => n.TreeNodeLabels)
            .WithOne(l => l.TreeNode)
            .HasForeignKey(n => n.TreeNodeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}