namespace TheGnouCommunity.UrlManager.Domain.AggregateModels.AnalyticsAggregate;

public interface IRedirectionRequestAnalyticsRepository
{
    Task AddDaily(int year, int month, int day, string host, string path, long? cityId);
    Task AddDailyError(int year, int month, int day, string host, string path, long? cityId);
    Task AddMonthly(int year, int month, string host, string path, long? cityId);
    Task AddMonthlyError(int year, int month, string host, string path, long? cityId);
    Task AddYearly(int year, string host, string path, long? cityId);
    Task AddYearlyError(int year, string host, string path, long? cityId);
}
