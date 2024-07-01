using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleNotes.Application.Abstractions;
using SimpleNotes.Application.Services;
using SimpleNotes.Infrastructure.DbContexts;
using SimpleNotes.Infrastructure.Repositories;
using SimpleNotes.Infrastructure.Services;

namespace SimpleNotes.Infrastructure.IntegrationTests.Fixtures;

public sealed class CreateNoteServiceFixture : IDisposable
{
    public CreateNoteServiceFixture()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddDbContext<INotesDbContext, NotesDbContext>(options =>
            options.UseInMemoryDatabase(databaseName: "TestDatabase"));
        serviceCollection.AddScoped<ICreateNoteService, CreateNoteService>();
        serviceCollection.AddScoped<INoteFactory, NoteFactory>();
        serviceCollection.AddScoped<INoteRepository, NoteRepository>();
        serviceCollection.AddSingleton<IIdGenerator, UuidV7IdGenerator>();

        ServiceProvider = serviceCollection.BuildServiceProvider();
    }

    public ServiceProvider ServiceProvider { get; }

    public void Dispose() => ServiceProvider.Dispose();
}