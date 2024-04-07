using Azure.Data.Tables;

namespace TheGnouCommunity.UrlManager.Infrastructure;

internal sealed class TableStorageHelper
{
    private readonly string _connectionString;

    public TableStorageHelper(string connectionString)
    {
        ArgumentNullException.ThrowIfNull(connectionString);

        _connectionString = connectionString;
    }

    public async Task<TableClient> GetTableClient(string tableName)
    {
        var tableServiceClient = CreateTableServiceClient();
        var tableClient = tableServiceClient.GetTableClient(tableName);
        await tableClient.CreateIfNotExistsAsync();
        return tableClient;
    }

    private TableServiceClient CreateTableServiceClient()
        => new TableServiceClient(_connectionString);
}