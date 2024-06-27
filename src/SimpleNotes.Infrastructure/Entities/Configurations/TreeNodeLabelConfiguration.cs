using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SimpleNotes.Infrastructure.Entities.Configurations;

public class TreeNodeLabelConfiguration : IEntityTypeConfiguration<TreeNodeLabel>
{
    public void Configure(EntityTypeBuilder<TreeNodeLabel> builder)
    {
        builder.HasKey(l => new { l.LabelId, l.TreeNodeId });
    }
}