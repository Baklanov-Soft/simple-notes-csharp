using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SimpleNotes.Infrastructure.Entities.Configurations;

public class LabelConfiguration : IEntityTypeConfiguration<Label>
{
    public void Configure(EntityTypeBuilder<Label> builder)
    {
        builder.HasKey(l => l.Id);
        builder.Property(l => l.Name).HasMaxLength(250);
        builder.Property(l => l.Color).HasMaxLength(7);
        builder.ToTable(l => l.HasCheckConstraint("CK_Notes_ColorRegEx", "\"Color\" ~* '^#[a-f0-9]{6}$'"));

        builder.HasMany(l => l.TreeNodeLabels)
            .WithOne(t => t.Label)
            .HasForeignKey(t => t.LabelId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}