using Microsoft.Extensions.DependencyInjection;
using TheGnouCommunity.UrlManager.Domain.AggregateModels.AnalyticsAggregate;
using TheGnouCommunity.UrlManager.Domain.AggregateModels.PathRedirectionAgregate;
using TheGnouCommunity.UrlManager.Services;

namespace TheGnouCommunity.UrlManager.Infrastructure.Extensions;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string storageConnectionString)
    {
        services.AddTransient<IPathRedirectionRepository>(_ => new PathRedirectionRepository(storageConnectionString));
        services.AddTransient<IRedirectionRequestAnalyticsRepository>(_ => new RedirectionRequestAnalyticsRepository(storageConnectionString));
        services.AddTransient<IServiceBus>(_ => new ServiceBus(storageConnectionString));

        return services;
    }
}
