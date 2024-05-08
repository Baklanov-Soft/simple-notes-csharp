using Microsoft.EntityFrameworkCore;
using SimpleNotes.Infrastructure.Enums;

namespace SimpleNotes.Infrastructure.Entities;

public class TreeNode
{
    public Guid Id { get; init; }

    public required string Name { get; set; }

    public required NodeType Type { get; init; }

    public required LTree Path { get; set; }

    public required HashSet<TreeNodeLabel> TreeNodeLabels { get; init; }
}