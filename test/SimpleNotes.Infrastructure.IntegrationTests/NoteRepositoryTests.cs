using SimpleNotes.Application.Abstractions;
using SimpleNotes.Infrastructure.IntegrationTests.Fixtures;

namespace SimpleNotes.Infrastructure.IntegrationTests;

public class NoteRepositoryTests(NoteRepositoryFixture seedFixture) : IClassFixture<NoteRepositoryFixture>
{
    private readonly INoteRepository _noteRepository = seedFixture.NoteRepository;

    [Fact]
    public async Task Get_note_by_id()
    {
        var noteId = Guid.ParseExact("0190593f-855b-7ef4-8c94-a777561bf853", "D");

        var note = await _noteRepository.GetByIdAsync(noteId);

        note.Should().NotBeNull();
    }
}