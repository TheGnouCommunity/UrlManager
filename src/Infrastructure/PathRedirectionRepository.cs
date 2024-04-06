using Azure.Data.Tables;
using Microsoft.Extensions.Options;
using TheGnouCommunity.UrlManager.Domain.AggregateModels.PathRedirectionAgregate;

namespace TheGnouCommunity.UrlManager.Infrastructure;

internal sealed class PathRedirectionRepository : IPathRedirectionRepository
{
    private readonly TableStorageHelper _tableStorageHelper;

    public PathRedirectionRepository(IOptions<StorageOptions> options)
    {
        ArgumentNullException.ThrowIfNull(options);

        _tableStorageHelper = new TableStorageHelper(options.Value);
    }

    public async Task<PathRedirection?> TryFindPathRedirectionByPath(string hostName, string path)
    {
        var tableClient = await GetPathRedirectionTableClient();
        var pathRedirectionEntityResponse = await tableClient.GetEntityIfExistsAsync<PathRedirectionEntity>(hostName, path);
        if (!pathRedirectionEntityResponse.HasValue ||
            pathRedirectionEntityResponse.Value is null)
        {
            return default;
        }

        var pathRedirectionEntity = pathRedirectionEntityResponse.Value;

        return new(
            pathRedirectionEntity.PartitionKey,
            pathRedirectionEntity.RowKey,
            pathRedirectionEntity.TargetUrl);
    }

    private Task<TableClient> GetPathRedirectionTableClient() => _tableStorageHelper.GetTableClient("PathRedirection");
}
