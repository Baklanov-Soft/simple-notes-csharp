using FluentResults;

namespace SimpleNotes.Application.Errors;

public class ParentNotFoundError(Guid id) : Error("Parent not found.")
{
    public Guid ParentId { get; } = id;
}