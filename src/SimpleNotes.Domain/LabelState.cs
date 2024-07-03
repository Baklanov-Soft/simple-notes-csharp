namespace SimpleNotes.Domain;

/// <summary>
/// Represents the state of a label.
/// </summary>
public enum LabelState
{
    /// <summary>
    /// The default state of a label.
    /// </summary>
    Default = 0,

    /// <summary>
    /// The state of a label when it is assigned.
    /// </summary>
    Assigned = 1,


    /// <summary>
    /// The state of a label when it is unassigned.
    /// </summary>
    Unassigned = 2
}