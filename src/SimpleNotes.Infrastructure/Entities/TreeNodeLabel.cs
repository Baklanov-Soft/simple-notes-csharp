namespace SimpleNotes.Infrastructure.Entities;

public class TreeNodeLabel : IEquatable<TreeNodeLabel>
{
    public Guid TreeNodeId { get; init; }
    public TreeNode TreeNode { get; init; } = null!;

    public Guid LabelId { get; init; }
    public Label Label { get; init; } = null!;

    public bool Equals(TreeNodeLabel? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return TreeNodeId.Equals(other.TreeNodeId) && LabelId.Equals(other.LabelId);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((TreeNodeLabel)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(TreeNodeId, LabelId);
    }

    public static bool operator ==(TreeNodeLabel? left, TreeNodeLabel? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(TreeNodeLabel? left, TreeNodeLabel? right)
    {
        return !Equals(left, right);
    }
}