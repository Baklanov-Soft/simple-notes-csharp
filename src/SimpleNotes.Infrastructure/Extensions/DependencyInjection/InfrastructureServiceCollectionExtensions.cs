using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using SimpleNotes.Infrastructure.DbContexts;
using SimpleNotes.Infrastructure.Enums;

namespace SimpleNotes.Infrastructure.Extensions.DependencyInjection;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddNotesDbContext(this IServiceCollection serviceCollection, string name) =>
        serviceCollection.AddDbContext<INotesDbContext, NotesDbContext>(
            (serviceProvider, options) =>
            {
                var connectionString = serviceProvider.GetRequiredService<IConfiguration>().GetConnectionString(name);
                var npgsqlDataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
                npgsqlDataSourceBuilder.MapEnum<NodeType>();
                var dataSource = npgsqlDataSourceBuilder.Build();

                options.UseNpgsql(dataSource);
            });
}