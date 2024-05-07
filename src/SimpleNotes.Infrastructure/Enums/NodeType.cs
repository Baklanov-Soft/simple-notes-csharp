namespace SimpleNotes.Infrastructure.Enums;

/// <summary>
/// Tree node discriminator.
/// </summary>
public enum NodeType
{
    /// <summary>
    /// A tree node containing child tree nodes.
    /// </summary>
    Folder,

    /// <summary>
    /// A tree node containing joinable text data.
    /// </summary>
    Note
}