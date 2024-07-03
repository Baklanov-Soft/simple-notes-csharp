using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleNotes.Application.Abstractions;
using SimpleNotes.Application.Services;
using SimpleNotes.Infrastructure.DbContexts;
using SimpleNotes.Infrastructure.Entities;
using SimpleNotes.Infrastructure.Enums;
using SimpleNotes.Infrastructure.Extensions.DependencyInjection;
using SimpleNotes.Infrastructure.Repositories;
using SimpleNotes.Infrastructure.Services;
using Testcontainers.PostgreSql;

namespace SimpleNotes.Infrastructure.IntegrationTests.Fixtures;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class CreateNoteServiceFixture : IAsyncLifetime
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
        serviceCollection.AddScoped<ICreateNoteService, CreateNoteService>();
        serviceCollection.AddScoped<INoteFactory, NoteFactory>();
        serviceCollection.AddScoped<INoteRepository, NoteRepository>();
        serviceCollection.AddSingleton<IIdGenerator, UuidV7IdGenerator>();

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
        
        var folderId = Guid.ParseExact("0190778d-118e-744d-a449-a0109ea1c7c5", "D");
        var folder = new TreeNode
        {
            Id = folderId,
            Name = "dev",
            Path = new LTree(folderId.ToString()),
            Type = NodeType.Folder
        };

        dbContext.TreeNodes.Add(folder);
        dbContext.SaveChanges();
    }
}