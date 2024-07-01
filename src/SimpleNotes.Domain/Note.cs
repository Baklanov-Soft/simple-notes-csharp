using FluentResults;
using SimpleNotes.Domain.Common;
using SimpleNotes.Domain.Errors;

namespace SimpleNotes.Domain;

public class Note(Guid id, string name, int maxNameLength, HashSet<Guid> labelIds) : Entity<Guid>(id)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Note"/> class.
    /// </summary>
    /// <param name="id">The unique identifier for the note.</param>
    /// <param name="name">The name of the note.</param>
    /// <param name="maxNameLength">The maximum length of the note name.</param>
    public Note(Guid id, string name, int maxNameLength) : this(id, name, maxNameLength, [])
    {
    }

    public string Name { get; private set; } = name;

    public string? Text { get; set; }
    public HashSet<Guid> LabelIds { get; } = labelIds;

    public Result ChangeName(string name) =>
        Result
            .FailIf(string.IsNullOrWhiteSpace(name), new NoteNameIsNullOrWhiteSpaceError())
            .Bind(() => Result.FailIf(name.Length > maxNameLength, new NoteNameLengthLimitExceededError()))
            .Bind(() =>
            {
                Name = name;
                return Result.Ok();
            });

    public Result AssignLabel(Guid labelId) =>
        Result.FailIf(!LabelIds.Add(labelId), new LabelAlreadyAssignedError(labelId));
}