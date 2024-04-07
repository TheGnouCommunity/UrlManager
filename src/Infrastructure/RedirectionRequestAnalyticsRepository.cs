using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;
using TheGnouCommunity.UrlManager.Domain.AggregateModels.AnalyticsAggregate;

namespace TheGnouCommunity.UrlManager.Infrastructure;

internal sealed class RedirectionRequestAnalyticsRepository : IRedirectionRequestAnalyticsRepository
{
    private readonly TableStorageHelper _tableStorageHelper;

    public RedirectionRequestAnalyticsRepository(string connectionString)
    {
        ArgumentNullException.ThrowIfNull(connectionString);

        _tableStorageHelper = new TableStorageHelper(connectionString);
    }

    public Task AddDaily(int year, int month, int day, string host, string path, long? cityId)
        => Increment(
            $"RedirectionRequestAnalyticsDaily{year:0000}{month:00}{day:00}",
            UrlEncoder.Default.Encode($"{host}/{path}"),
            cityId);

    public Task AddDailyError(int year, int month, int day, string host, string path, long? cityId)
        => Increment(
            $"RedirectionRequestAnalyticsDailyErrors{year:0000}{month:00}{day:00}",
            UrlEncoder.Default.Encode($"{host}/{path}"),
            cityId);

    public Task AddMonthly(int year, int month, string host, string path, long? cityId)
        => Increment(
            $"RedirectionRequestAnalyticsMonthly{year:0000}{month:00}",
            UrlEncoder.Default.Encode($"{host}/{path}"),
            cityId);

    public Task AddMonthlyError(int year, int month, string host, string path, long? cityId)
        => Increment(
            $"RedirectionRequestAnalyticsMonthlyErrors{year:0000}{month:00}",
            UrlEncoder.Default.Encode($"{host}/{path}"),
            cityId);

    public Task AddYearly(int year, string host, string path, long? cityId)
        => Increment(
            $"RedirectionRequestAnalyticsYearly{year:0000}",
            UrlEncoder.Default.Encode($"{host}/{path}"),
            cityId);

    public Task AddYearlyError(int year, string host, string path, long? cityId)
        => Increment(
            $"RedirectionRequestAnalyticsYearlyErrors{year:0000}",
            UrlEncoder.Default.Encode($"{host}/{path}"),
            cityId);

    public async Task Increment(string tableName, string partitionKey, long? cityId)
    {
        var tableClient = await _tableStorageHelper.GetTableClient(tableName);
        string rowKey = cityId?.ToString() ?? string.Empty;
        var redirectionRequestAnalyticsEntityResponse = await tableClient.GetEntityIfExistsAsync<RedirectionRequestAnalyticsEntity>(partitionKey, rowKey);
        if (!redirectionRequestAnalyticsEntityResponse.HasValue ||
            redirectionRequestAnalyticsEntityResponse.Value is null)
        {
            _ = await tableClient.AddEntityAsync(
                new RedirectionRequestAnalyticsEntity
                {
                    PartitionKey = partitionKey,
                    RowKey = rowKey,
                    Count = 1,
                });

            return;
        }

        _ = await tableClient.UpdateEntityAsync(
            new RedirectionRequestAnalyticsEntity
            {
                PartitionKey = partitionKey,
                RowKey = rowKey,
                Count = redirectionRequestAnalyticsEntityResponse.Value.Count + 1,
            },
            redirectionRequestAnalyticsEntityResponse.Value.ETag);
    }
}
