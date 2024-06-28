using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using SimpleNotes.Application.Abstractions;
using SimpleNotes.Infrastructure.DbContexts;
using SimpleNotes.Infrastructure.Enums;
using SimpleNotes.Infrastructure.Repositories;
using SimpleNotes.Infrastructure.Services;

namespace SimpleNotes.Infrastructure.Extensions.DependencyInjection;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddNotesDbContext(this IServiceCollection serviceCollection,
        string? connectionString)
    {
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
        dataSourceBuilder.MapEnum<NodeType>();
        var dataSource = dataSourceBuilder.Build();

        return serviceCollection.AddDbContext<INotesDbContext, NotesDbContext>(options =>
            options.UseNpgsql(dataSource));
    }
    
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddSingleton<INoteFactory, NoteFactory>()
            .AddScoped<ICreateNoteService, CreateNoteService>()
            .AddScoped<INoteRepository, NoteRepository>();
    }
}