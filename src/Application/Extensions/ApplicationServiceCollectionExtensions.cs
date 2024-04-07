using FluentResults;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TheGnouCommunity.UrlManager.Application.Commands;

namespace TheGnouCommunity.UrlManager.Application.Extensions;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, Action<OptionsBuilder<GeoIP2Options>>? configureGeoIP2)
    {
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(typeof(ApplicationServiceCollectionExtensions).Assembly);
            cfg.AddBehavior<IPipelineBehavior<RedirectionRequest, Result<string>>, RedirectionRequestHandlerResultBehavior>();
        });

        var geoIP2OptionsBuilder = services.AddOptions<GeoIP2Options>();
        if (configureGeoIP2 is not null)
        {
            configureGeoIP2(geoIP2OptionsBuilder);
        }

        return services;
    }
}
