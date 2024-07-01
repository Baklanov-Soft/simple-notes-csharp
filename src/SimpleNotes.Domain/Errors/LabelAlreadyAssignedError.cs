using FluentResults;

namespace SimpleNotes.Domain.Errors;

public class LabelAlreadyAssignedError(Guid labelId) : Error(MessageText)
{
    private const string MessageText = "Label already assigned.";

    public Guid LabelId { get; } = labelId;
}