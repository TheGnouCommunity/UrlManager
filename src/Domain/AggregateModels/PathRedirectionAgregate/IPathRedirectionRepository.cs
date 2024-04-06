namespace TheGnouCommunity.UrlManager.Domain.AggregateModels.PathRedirectionAgregate;

public interface IPathRedirectionRepository
{
    Task<PathRedirection?> TryFindPathRedirectionByPath(string hostName, string path);
}