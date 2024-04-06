using Azure.Data.Tables;

namespace TheGnouCommunity.UrlManager.Infrastructure;

internal sealed class TableStorageHelper
{
    private readonly StorageOptions _options;

    public TableStorageHelper(StorageOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);

        _options = options;
    }

    public async Task<TableClient> GetTableClient(string tableName)
    {
        var tableServiceClient = CreateTableServiceClient();
        var tableClient = tableServiceClient.GetTableClient(tableName);
        await tableClient.CreateIfNotExistsAsync();
        return tableClient;
    }

    private TableServiceClient CreateTableServiceClient()
        => new TableServiceClient(
            new Uri($"https://{_options.AccountName}.table.core.windows.net"),
            new TableSharedKeyCredential(_options.AccountName, _options.StorageAccountKey));
}