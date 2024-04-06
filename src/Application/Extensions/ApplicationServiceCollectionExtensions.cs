using FluentResults;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TheGnouCommunity.UrlManager.Application.Commands;

namespace TheGnouCommunity.UrlManager.Application.Extensions;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(typeof(ApplicationServiceCollectionExtensions).Assembly);
            cfg.AddBehavior<IPipelineBehavior<RedirectionRequest, Result<string>>, RedirectionRequestHandlerResultBehavior>();
        });

        return services;
    }
}
