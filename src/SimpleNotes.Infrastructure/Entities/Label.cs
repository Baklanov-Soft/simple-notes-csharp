namespace SimpleNotes.Infrastructure.Entities;

public class Label
{
    public required Guid Id { get; init; }
    public required string Name { get; set; }
    public required string Color { get; set; }
    
    public HashSet<TreeNodeLabel>? TreeNodeLabels { get; set; }
}