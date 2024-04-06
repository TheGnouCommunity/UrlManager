using FluentResults;
using MediatR;
using TheGnouCommunity.UrlManager.Domain.Messaging;
using TheGnouCommunity.UrlManager.Services;

namespace TheGnouCommunity.UrlManager.Application.Commands;

internal sealed class RedirectionRequestHandlerResultBehavior : IPipelineBehavior<RedirectionRequest, Result<string>>
{
    private readonly IServiceBus _serviceBus;

    public RedirectionRequestHandlerResultBehavior(IServiceBus serviceBus)
    {
        ArgumentNullException.ThrowIfNull(serviceBus);

        _serviceBus = serviceBus;
    }

    public async Task<Result<string>> Handle(RedirectionRequest request, RequestHandlerDelegate<Result<string>> next, CancellationToken cancellationToken)
    {
        var result = await next.Invoke();
        if (result.IsFailed)
        {
            await _serviceBus.Publish(new RedirectionRequestFailed
            {
                Host = request.Host,
                Path = request.Path,
                IPAddress = request.IPAddress?.ToString(),
                Errors = result.Errors.Select(_ => _.Message).ToList(),
            });

            return result;
        }

        await _serviceBus.Publish(new RedirectionRequestSucceeded
        {
            Host = request.Host,
            Path = request.Path,
            IPAddress = request.IPAddress?.ToString(),
        });

        return result;
    }
}