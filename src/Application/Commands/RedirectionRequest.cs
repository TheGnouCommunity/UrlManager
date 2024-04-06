using FluentResults;
using MediatR;
using System.Net;

namespace TheGnouCommunity.UrlManager.Application.Commands;

public sealed class RedirectionRequest : IRequest<Result<string>>
{
    public string Host { get; }
    public string Path { get; }
    public IPAddress? IPAddress { get; }

    public RedirectionRequest(string host, string path, IPAddress? ipAddress)
    {
        ArgumentNullException.ThrowIfNull(host);
        ArgumentNullException.ThrowIfNull(path);

        Host = host;
        Path = path;
        IPAddress = ipAddress;
    }
}