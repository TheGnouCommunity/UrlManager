using Azure;
using Azure.Data.Tables;

namespace TheGnouCommunity.UrlManager.Infrastructure;

internal record PathRedirectionEntity : ITableEntity
{
    public string RowKey { get; set; } = default!;

    public string PartitionKey { get; set; } = default!;

    public string TargetUrl { get; set; } = default!;

    public ETag ETag { get; set; } = default!;

    public DateTimeOffset? Timestamp { get; set; } = default!;
}
