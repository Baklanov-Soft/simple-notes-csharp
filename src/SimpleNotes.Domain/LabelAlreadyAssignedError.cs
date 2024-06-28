using FluentResults;

namespace SimpleNotes.Domain;

public class LabelAlreadyAssignedError(Guid labelId) : Error(MessageText)
{
    private const string MessageText = "Label already assigned.";

    public Guid LabelId { get; } = labelId;
}