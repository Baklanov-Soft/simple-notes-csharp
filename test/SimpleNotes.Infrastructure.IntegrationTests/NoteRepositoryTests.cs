using Microsoft.Extensions.DependencyInjection;
using SimpleNotes.Application.Abstractions;
using SimpleNotes.Infrastructure.IntegrationTests.Fixtures;

namespace SimpleNotes.Infrastructure.IntegrationTests;

public sealed class NoteRepositoryTests(NoteRepositoryFixture fixture) : IClassFixture<NoteRepositoryFixture>
{
    private readonly INoteRepository _noteRepository = fixture.Services.GetRequiredService<INoteRepository>();

    [Fact]
    public async Task Get_note_by_id()
    {
        var noteId = Guid.ParseExact("0190593f-855b-7ef4-8c94-a777561bf853", "D");

        var note = await _noteRepository.GetByIdAsync(noteId);

        note.Should().NotBeNull();
    }

    [Fact]
    public async Task Get_path_by_id()
    {
        var noteId = Guid.ParseExact("0190593f-855b-7ef4-8c94-a777561bf853", "D");

        var pathResult = await _noteRepository.GetFolderPathAsync(noteId);

        pathResult.IsSuccess.Should().BeTrue();
        pathResult.Value.Should().Be("0190593f-855b-7ef4-8c94-a777561bf853");
    }

    [Fact]
    public async Task Delete_note()
    {
        var noteId = Guid.ParseExact("0190593f-855b-7ef4-8c94-a777561bf853", "D");
        await _noteRepository.DeleteAsync(noteId);

        var note = await _noteRepository.GetByIdAsync(noteId);
        note.Should().BeNull();
    }
}