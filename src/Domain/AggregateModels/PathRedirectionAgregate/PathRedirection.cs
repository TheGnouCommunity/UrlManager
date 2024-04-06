namespace TheGnouCommunity.UrlManager.Domain.AggregateModels.PathRedirectionAgregate;

public class PathRedirection
{
    public string Host { get; }
    public string Path { get; }
    public string TargetUrl { get; }

    public PathRedirection(
        string host,
        string path,
        string targetUrl)
    {
        Host = host;
        Path = path;
        TargetUrl = targetUrl;
    }
}
