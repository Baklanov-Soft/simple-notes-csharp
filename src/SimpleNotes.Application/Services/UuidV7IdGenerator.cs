using SimpleNotes.Application.Abstractions;
using UUIDNext;

namespace SimpleNotes.Application.Services;

public class UuidV7IdGenerator : IIdGenerator
{
    public Guid NewId()
    {
        return Uuid.NewSequential();
    }
}