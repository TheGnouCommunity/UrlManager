using Azure.Data.Tables;
using Domain;
using Microsoft.Extensions.Options;

namespace Infrastructure;

internal sealed class PathRedirectionRepository : IPathRedirectionRepository
{
    private readonly TableStorageHelper _tableStorageHelper;

    public PathRedirectionRepository(IOptions<TableStorageOptions> options)
    {
        ArgumentNullException.ThrowIfNull(options);

        _tableStorageHelper = new TableStorageHelper(options.Value);
    }

    public async Task<PathRedirection?> TryFindPathRedirectionByPath(string hostName, string path)
    {
        var tableClient = GetPathRedirectionTableClient();
        await tableClient.CreateIfNotExistsAsync();
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

    private TableClient GetPathRedirectionTableClient() => _tableStorageHelper.GetTableClient("PathRedirection");
}
