using FluentResults;

namespace SimpleNotes.Application.Errors;

public class NoteNotFoundError(Guid id) : Error("Note not found.")
{
    public Guid ParentId { get; } = id;
}