using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TheGnouCommunity.UrlManager.Domain.AggregateModels.PathRedirectionAgregate;
using TheGnouCommunity.UrlManager.Services;

namespace TheGnouCommunity.UrlManager.Infrastructure.Extensions;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, Action<OptionsBuilder<StorageOptions>>? configureStorage)
    {
        services.AddTransient<IPathRedirectionRepository, PathRedirectionRepository>();
        services.AddTransient<IServiceBus, ServiceBus>();

        var tableStorageOptionsBuilder = services.AddOptions<StorageOptions>();
        if (configureStorage is not null)
        {
            configureStorage(tableStorageOptionsBuilder);
        }

        return services;
    }
}
