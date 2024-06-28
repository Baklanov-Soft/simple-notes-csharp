namespace SimpleNotes.Application.Models;

public class CreateNoteDto
{
    public required string Name { get; init; }
    public required string Text { get; init; }
    public required HashSet<Guid>? LabelIds { get; init; }
}
