namespace SimpleNotes.Infrastructure.Entities;

public class Label
{
    public required Guid Id { get; init; }
    public required string Name { get; set; }
    public required string Color { get; set; }
    
    public required HashSet<TreeNodeLabel> TreeNodeLabels { get; init; }
}