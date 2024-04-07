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

internal record RedirectionRequestAnalyticsEntity : ITableEntity
{
    public string RowKey { get; set; } = default!;

    public string PartitionKey { get; set; } = default!;

    public int Count { get; set; } = 0;

    public ETag ETag { get; set; } = default!;

    public DateTimeOffset? Timestamp { get; set; } = default!;
}
