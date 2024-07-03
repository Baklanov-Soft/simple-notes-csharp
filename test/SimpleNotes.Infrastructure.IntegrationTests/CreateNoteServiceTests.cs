using Microsoft.Extensions.DependencyInjection;
using SimpleNotes.Application.Abstractions;
using SimpleNotes.Application.Models;
using SimpleNotes.Infrastructure.DbContexts;
using SimpleNotes.Infrastructure.IntegrationTests.Fixtures;

namespace SimpleNotes.Infrastructure.IntegrationTests;

public class CreateNoteServiceTests(CreateNoteServiceFixture createNoteServiceFixture)
    : IClassFixture<CreateNoteServiceFixture>
{
    private readonly ICreateNoteService _createNoteService =
        createNoteServiceFixture.Services.GetRequiredService<ICreateNoteService>();

    private readonly INotesDbContext _notesDbContext =
        createNoteServiceFixture.Services.GetRequiredService<INotesDbContext>();

    [Fact]
    public async Task Create_top_level_note()
    {
        var createNoteDto = new CreateNoteDto
        {
            Name = "test note",
            Text = "test text"
        };

        var (id, _, _) = await _createNoteService.CreateAsync(createNoteDto, null, CancellationToken.None);

        var hasNote = _notesDbContext.Notes.Any(note => note.Id == id);
        hasNote.Should().BeTrue();
    }

    [Fact]
    public async Task Create_note_with_invalid_parent()
    {
        var createNoteDto = new CreateNoteDto
        {
            Name = "test note",
            Text = "test text"
        };
        var parentId = Guid.ParseExact("0190593f-855b-7ef4-8c94-a777561bf853", "D");

        var (id, _, _) = await _createNoteService.CreateAsync(createNoteDto, parentId, CancellationToken.None);

        var note = _notesDbContext.Notes.FirstOrDefault(note => note.Id == id);
        note.Should().NotBeNull();
        note!.Path.ToString().Should().Be(expected: note.Id.ToString());
    }

    [Fact]
    public async Task Create_note_within_folder()
    {
        var createNoteDto = new CreateNoteDto
        {
            Name = "test note",
            Text = "test text"
        };
        var parentId = Guid.ParseExact("0190778d-118e-744d-a449-a0109ea1c7c5", "D");

        var (id, _, _) = await _createNoteService.CreateAsync(createNoteDto, parentId, CancellationToken.None);

        var note = _notesDbContext.Notes.FirstOrDefault(note => note.Id == id);
        note.Should().NotBeNull();
        note!.Path.ToString().Should().Be(expected: $"{parentId}.{note.Id}");
    }
}