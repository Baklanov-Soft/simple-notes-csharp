using Microsoft.EntityFrameworkCore;
using SimpleNotes.Infrastructure.Enums;

namespace SimpleNotes.Infrastructure.Entities;

public class TreeNode
{
    public required Guid Id { get; init; }

    public required string Name { get; set; }

    public required NodeType Type { get; init; }

    public LTree Path { get; set; } = null!;

    public HashSet<TreeNodeLabel>? TreeNodeLabels { get; set; }
}