using FluentResults;

namespace SimpleNotes.Domain;

public class LabelAlreadyAssignedError(Label label) : Error(MessageText)
{
    private const string MessageText = "Label already assigned.";

    public Label Label { get; } = label;
}