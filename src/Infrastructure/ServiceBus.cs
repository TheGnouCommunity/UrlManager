using Azure.Storage.Queues;
using System.Text.Json;
using System.Text.RegularExpressions;
using TheGnouCommunity.UrlManager.Services;

namespace TheGnouCommunity.UrlManager.Infrastructure;

internal sealed class ServiceBus : IServiceBus
{
    private readonly QueueStorageHelper _queueStorageHelper;

    public ServiceBus(string connectionString)
    {
        ArgumentNullException.ThrowIfNull(connectionString);

        _queueStorageHelper = new QueueStorageHelper(connectionString);
    }

    public async Task Publish<T>(T message)
    {
        var queueClient = await GetQueueClient<T>();
        _ = await queueClient.SendMessageAsync(
            JsonSerializer.Serialize(message));
    }

    private Task<QueueClient> GetQueueClient<T>()
    {
        var queueName = Regex.Replace(typeof(T).Name, "(?<!^)([A-Z])", "-$1").ToLower();
        return _queueStorageHelper.GetQueueClient(queueName);
    }
}
