using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleNotes.Application.Abstractions;
using SimpleNotes.Infrastructure.DbContexts;
using SimpleNotes.Infrastructure.Entities;
using SimpleNotes.Infrastructure.Enums;
using SimpleNotes.Infrastructure.Extensions.DependencyInjection;
using SimpleNotes.Infrastructure.Repositories;
using Testcontainers.PostgreSql;

namespace SimpleNotes.Infrastructure.IntegrationTests.Fixtures;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class NoteRepositoryFixture : IAsyncLifetime
{
    private readonly PostgreSqlContainer _container = new PostgreSqlBuilder()
        .WithImage("postgres:16-alpine3.18")
        .Build();

    public ServiceProvider? Services { get; private set; }
    
    public async Task InitializeAsync()
    {
        await _container.StartAsync();

        var serviceCollection = new ServiceCollection();
        serviceCollection.AddNotesDbContext(_container.GetConnectionString());
        serviceCollection.AddScoped<INoteRepository, NoteRepository>();

        Services = serviceCollection.BuildServiceProvider();

        CreateAndSeedSchema();
    }

    public async Task DisposeAsync()
    {
        await Services.DisposeAsync();
        await _container.DisposeAsync().AsTask();
    }

    private void CreateAndSeedSchema()
    {
        using var scope = Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<INotesDbContext>();
        dbContext.EnsureCreated();

        var noteId = Guid.ParseExact("0190593f-855b-7ef4-8c94-a777561bf853", "D");
        var labelId = Guid.ParseExact("01905940-1776-76b4-b8a7-669f8fd2853c", "D");
        var folderId = Guid.ParseExact("01907847-3a2d-7bc6-8667-54072be7aa07", "D");
        var note = new Note
        {
            Id = noteId,
            Name = "Test note 1",
            Path = new LTree($"{folderId}.{noteId}"),
            Type = NodeType.Note,
            Text = "Text for test note 1",
            TreeNodeLabels =
            [
                new TreeNodeLabel
                {
                    TreeNodeId = noteId,
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
        var folder = new TreeNode
        {
            Id = folderId,
            Name = "dev",
            Path = new LTree(folderId.ToString()),
            Type = NodeType.Folder
        };
        
        dbContext.TreeNodes.Add(folder);
        dbContext.Notes.Add(note);
        dbContext.SaveChanges();
    }
}