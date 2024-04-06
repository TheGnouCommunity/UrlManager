using Azure;
using Azure.Data.Tables;

namespace Infrastructure;

internal record PathRedirectionEntity : ITableEntity
{
    public string RowKey { get; set; } = default!;

    public string PartitionKey { get; set; } = default!;

    public string TargetUrl { get; set; } = default!;

    public ETag ETag { get; set; } = default!;

    public DateTimeOffset? Timestamp { get; set; } = default!;
}
