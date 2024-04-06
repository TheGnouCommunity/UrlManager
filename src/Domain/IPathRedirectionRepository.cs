namespace Domain;

public interface IPathRedirectionRepository
{
    Task<PathRedirection?> TryFindPathRedirectionByPath(string hostName, string path);
}