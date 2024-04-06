using Azure.Storage;
using Azure.Storage.Queues;

namespace TheGnouCommunity.UrlManager.Infrastructure;

internal sealed class QueueStorageHelper
{
    private readonly StorageOptions _options;

    public QueueStorageHelper(StorageOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);

        _options = options;
    }

    public async Task<QueueClient> GetQueueClient(string queueName)
    {
        var queueServiceClient = CreateQueueServiceClient();
        var queueClient = queueServiceClient.GetQueueClient(queueName);
        await queueClient.CreateIfNotExistsAsync();
        return queueClient; ;
    }

    private QueueServiceClient CreateQueueServiceClient()
        => new QueueServiceClient(
            new Uri($"https://{_options.AccountName}.queue.core.windows.net"),
            new StorageSharedKeyCredential(_options.AccountName, _options.StorageAccountKey));

}
