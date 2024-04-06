using Domain;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure.Extensions;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, Action<OptionsBuilder<TableStorageOptions>>? configureTableStorage)
    {
        services.AddTransient<IPathRedirectionRepository, PathRedirectionRepository>();

        var tableStorageOptionsBuilder = services.AddOptions<TableStorageOptions>();
        if (configureTableStorage is not null)
        {
            configureTableStorage(tableStorageOptionsBuilder);
        }

        return services;
    }
}
