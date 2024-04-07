using MediatR;

namespace TheGnouCommunity.UrlManager.Application.Commands;

public sealed class  CollectAnalysticsRequest : IRequest
{
    public string Host { get; }
    public string Path { get; }
    public string? IPAddress { get; }
    public DateTime RequestTime { get; }
    public string[]? Errors { get; }

    public CollectAnalysticsRequest(string host, string path, string? iPAddress, DateTime requestTime, string[]? errors = default)
    {
        Host = host;
        Path = path;
        IPAddress = iPAddress;
        RequestTime = requestTime;
        Errors = errors;
    }
}