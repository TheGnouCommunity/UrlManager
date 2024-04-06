using Domain;
using FluentResults;
using MediatR;

namespace Application.Commands;

internal sealed class RedirectionRequestHandler : IRequestHandler<RedirectionRequest, Result<string>>
{
    private readonly IPathRedirectionRepository _pathRedirectionRepository;

    public RedirectionRequestHandler(IPathRedirectionRepository pathRedirectionRepository)
    {
        _pathRedirectionRepository = pathRedirectionRepository;
    }

    public async Task<Result<string>> Handle(RedirectionRequest request, CancellationToken cancellationToken)
    {
        var pathRedirection = await _pathRedirectionRepository.TryFindPathRedirectionByPath(request.Host, request.Path);
        if (pathRedirection is null)
        {
            return Result.Fail("No path redirection found.");
        }

        return pathRedirection.TargetUrl;
    }
}