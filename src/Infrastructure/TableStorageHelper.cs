using Azure.Data.Tables;

namespace Infrastructure;

internal sealed class TableStorageHelper
{
    private readonly TableStorageOptions _options;

    public TableStorageHelper(TableStorageOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);

        _options = options;
    }

    public TableClient GetTableClient(string tableName)
    {
        var tableServiceClient = CreateTableServiceClient();
        return tableServiceClient.GetTableClient(tableName);
    }

    private TableServiceClient CreateTableServiceClient()
        => new TableServiceClient(
            new Uri(_options.StorageUri),
            new TableSharedKeyCredential(_options.AccountName, _options.StorageAccountKey));

}
