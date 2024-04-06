namespace Domain;

public class PathRedirection
{
    public string HostName { get; }
    public string Path { get; }
    public string TargetUrl { get; }

    public PathRedirection(
        string hostName,
        string path,
        string targetUrl)
    {
        HostName = hostName;
        Path = path;
        TargetUrl = targetUrl;
    }
}
