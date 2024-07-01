using FluentResults;

namespace SimpleNotes.Domain.Errors;

public class NoteNameLengthLimitExceededError : Error
{
    private const string MessageText = "Note name length limit exceeded.";

    public NoteNameLengthLimitExceededError() : base(MessageText)
    {
    }

    public NoteNameLengthLimitExceededError(IError causedBy) : base(MessageText, causedBy)
    {
    }
}