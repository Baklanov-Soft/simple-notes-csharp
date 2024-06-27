using Microsoft.EntityFrameworkCore;
using SimpleNotes.Application.Abstractions;
using SimpleNotes.Infrastructure.DbContexts;
using SimpleNotes.Infrastructure.Entities;
using SimpleNotes.Infrastructure.Enums;
using SimpleNotes.Infrastructure.Repositories;

namespace SimpleNotes.Infrastructure.IntegrationTests.Fixtures;

public sealed class NoteRepositoryFixture : IDisposable
{
    private readonly NotesDbContext _dbContext;

    public NoteRepositoryFixture()
    {
        var options = new DbContextOptionsBuilder<NotesDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _dbContext = new NotesDbContext(options);
        SeedTestData();

        NoteRepository = new NoteRepository(_dbContext);
    }

    public INoteRepository NoteRepository { get; }

    public void Dispose() => _dbContext.Dispose();

    private void SeedTestData()
    {
        var noteId = Guid.ParseExact("0190593f-855b-7ef4-8c94-a777561bf853", "D");
        var labelId = Guid.ParseExact("01905940-1776-76b4-b8a7-669f8fd2853c", "D");
        var note = new Note
        {
            Id = noteId,
            Name = "Test note 1",
            Path = new LTree(noteId.ToString()),
            Type = NodeType.Note,
            Text = "Text for test note 1",
            TreeNodeLabels =
            [
                new TreeNodeLabel
                {
                    TreeNodeId = noteId,
                    TreeNode = null!,
                    LabelId = labelId,
                    Label = new Label
                    {
                        Id = labelId,
                        Name = "Important",
                        Color = "#ff0000"
                    }
                }
            ]
        };

        _dbContext.Notes.Add(note);
        _dbContext.SaveChanges();
    }
}