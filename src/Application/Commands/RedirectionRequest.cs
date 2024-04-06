using FluentResults;
using MediatR;

namespace Application.Commands;

public sealed class RedirectionRequest : IRequest<Result<string>>
{
    public string Host { get; }
    public string Path { get; }

    public RedirectionRequest(string host, string path)
    {
        ArgumentNullException.ThrowIfNull(host);
        ArgumentNullException.ThrowIfNull(path);

        Host = host;
        Path = path;
    }
}