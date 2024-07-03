using Microsoft.Extensions.DependencyInjection;
using SimpleNotes.Application.Abstractions;
using SimpleNotes.Application.Services;

namespace SimpleNotes.Application.Extensions.DependencyInjection;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        return services
            .AddSingleton<IIdGenerator, UuidV7IdGenerator>()
            .AddScoped<IAssignLabelService, AssignLabelService>();
    }
}