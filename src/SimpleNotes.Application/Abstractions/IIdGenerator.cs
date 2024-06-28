namespace SimpleNotes.Application.Abstractions;

public interface IIdGenerator
{
    Guid NewId();
}