using FluentResults;
using MediatR;
using TheGnouCommunity.UrlManager.Application.Errors;
using TheGnouCommunity.UrlManager.Domain.AggregateModels.PathRedirectionAgregate;

namespace TheGnouCommunity.UrlManager.Application.Commands;

internal sealed class RedirectionRequestHandler : IRequestHandler<RedirectionRequest, Result<string>>
{
    private readonly IPathRedirectionRepository _pathRedirectionRepository;

    public RedirectionRequestHandler(IPathRedirectionRepository pathRedirectionRepository)
    {
        ArgumentNullException.ThrowIfNull(pathRedirectionRepository);

        _pathRedirectionRepository = pathRedirectionRepository;
    }

    public async Task<Result<string>> Handle(RedirectionRequest request, CancellationToken cancellationToken)
    {
        var pathRedirection = await _pathRedirectionRepository.TryFindPathRedirectionByPath(request.Host, request.Path);
        if (pathRedirection is null)
        {
            return Result.Fail(new PathRedirectionNotFoundError(request.Host, request.Path));
        }

        return pathRedirection.TargetUrl;
    }
}