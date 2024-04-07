using Azure.Storage.Queues;

namespace TheGnouCommunity.UrlManager.Infrastructure;

internal sealed class QueueStorageHelper
{
    private readonly string _connectionString;

    public QueueStorageHelper(string connectionString)
    {
        ArgumentNullException.ThrowIfNull(connectionString);

        _connectionString = connectionString;
    }

    public async Task<QueueClient> GetQueueClient(string queueName)
    {
        var queueServiceClient = CreateQueueServiceClient();
        var queueClient = queueServiceClient.GetQueueClient(queueName);
        await queueClient.CreateIfNotExistsAsync();
        return queueClient;
    }

    private QueueServiceClient CreateQueueServiceClient()
        => new QueueServiceClient(_connectionString, new QueueClientOptions
        {
            MessageEncoding = QueueMessageEncoding.Base64
        });

}
