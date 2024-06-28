using UUIDNext;

namespace SimpleNotes.Application.Abstractions;

public class UuidV7IdGenerator : IIdGenerator
{
    public Guid NewId()
    {
        return Uuid.NewSequential();
    }
}