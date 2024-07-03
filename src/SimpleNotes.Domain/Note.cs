using FluentResults;
using SimpleNotes.Domain.Common;
using SimpleNotes.Domain.Errors;

namespace SimpleNotes.Domain;

/// <summary>
/// Represents a note.
/// </summary>
public class Note(Guid id, string name, int maxNameLength) : Entity<Guid>(id)
{
    /// <summary>
    /// The name of the note.
    /// </summary>
    public string Name { get; private set; } = name;

    /// <summary>
    /// The text of the note.
    /// </summary>
    public string? Text { get; set; }
    
    /// <summary>
    /// Changes the name of the note.
    /// </summary>
    /// <param name="name">The new name of the note.</param>
    /// <returns>A result indicating the success or failure of the operation.</returns>
    public Result ChangeName(string name) =>
        Result
            .FailIf(string.IsNullOrWhiteSpace(name), new NoteNameIsNullOrWhiteSpaceError())
            .Bind(() => Result.FailIf(name.Length > maxNameLength, new NoteNameLengthLimitExceededError()))
            .Bind(() =>
            {
                Name = name;
                return Result.Ok();
            });
}