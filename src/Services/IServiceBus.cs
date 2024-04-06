namespace TheGnouCommunity.UrlManager.Services;

public interface IServiceBus
{
    Task Publish<T>(T message);
}