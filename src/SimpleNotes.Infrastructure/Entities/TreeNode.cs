using Microsoft.EntityFrameworkCore;
using SimpleNotes.Infrastructure.Enums;

namespace SimpleNotes.Infrastructure.Entities;

public class TreeNode
{
    public required Guid Id { get; init; }

    public required string Name { get; set; }

    public required NodeType Type { get; init; }

    public required LTree Path { get; set; }

    public HashSet<TreeNodeLabel>? TreeNodeLabels { get; set; }
}