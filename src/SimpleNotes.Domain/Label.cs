using SimpleNotes.Domain.Common;

namespace SimpleNotes.Domain;

public class Label(Guid id, string name, string color) : Entity<Guid>(id)
{
    public string Name { get; } = name;
    public string Color { get; } = color;
}