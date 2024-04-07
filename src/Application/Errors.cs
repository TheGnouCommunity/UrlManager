using FluentResults;

namespace TheGnouCommunity.UrlManager.Application.Errors;

public sealed class PathRedirectionNotFoundError : Error
{
    public PathRedirectionNotFoundError(string host, string path) : base("Path redirection found.") { WithMetadata(nameof(host), host); WithMetadata(nameof(path), path); }
}
