using Microsoft.EntityFrameworkCore;
using SimpleNotes.Infrastructure.Enums;

namespace SimpleNotes.Infrastructure.Entities;

public class TreeNode
{
    public required Guid Id { get; init; }

    public string Name { get; set; } = null!;

    public NodeType Type { get; init; }

    public LTree Path { get; set; }

    public HashSet<TreeNodeLabel>? TreeNodeLabels { get; set; }
}