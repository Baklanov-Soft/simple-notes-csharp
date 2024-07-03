namespace SimpleNotes.Application.Models;

public record ReadNoteDto(Guid Id, string Name, string? Text);
