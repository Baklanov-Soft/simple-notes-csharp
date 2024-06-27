using FluentResults;

namespace SimpleNotes.Domain;

/// <summary>
/// Represents an error that occurs when the note name is null or whitespace.
/// </summary>
public class NoteNameIsNullOrWhiteSpaceError : Error
{
    private const string MessageText = "Note name is null or whitespace.";
    
    /// <summary>
    /// Initializes a new instance of the <see cref="NoteNameIsNullOrWhiteSpaceError"/> class.
    /// </summary>
    public NoteNameIsNullOrWhiteSpaceError() : base(MessageText)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NoteNameIsNullOrWhiteSpaceError"/> class with a specified cause.
    /// </summary>
    /// <param name="causedBy">The error that caused this instance.</param>
    public NoteNameIsNullOrWhiteSpaceError(IError causedBy) : base(MessageText, causedBy)
    {
    }
}