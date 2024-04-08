using MaxMind.GeoIP2;
using MaxMind.GeoIP2.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TheGnouCommunity.UrlManager.Domain.AggregateModels.AnalyticsAggregate;

namespace TheGnouCommunity.UrlManager.Application.Commands;

internal sealed class CollectAnalyticsRequestHandler : IRequestHandler<CollectAnalysticsRequest>
{
    private readonly ILogger _logger;
    private readonly GeoIP2Options _options;
    private readonly IRedirectionRequestAnalyticsRepository _redirectionRequestAnalyticsRepository;

    public CollectAnalyticsRequestHandler(ILogger<CollectAnalyticsRequestHandler> logger, IOptions<GeoIP2Options> options, IRedirectionRequestAnalyticsRepository redirectionRequestAnalyticsRepository)
    {
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(options);
        ArgumentNullException.ThrowIfNull(redirectionRequestAnalyticsRepository);

        _logger = logger;
        _options = options.Value;
        _redirectionRequestAnalyticsRepository = redirectionRequestAnalyticsRepository;
    }

    public async Task Handle(CollectAnalysticsRequest request, CancellationToken cancellationToken)
    {
        long? cityId = default;
        if (request.IPAddress is not null)
        {
            var client = new WebServiceClient(_options.AccountId, _options.LicenseKey, host: "geolite.info");
            try
            {
                var cityResponse = await client.CityAsync(request.IPAddress);
                cityId = cityResponse.City.GeoNameId;
            }
            catch (AddressNotFoundException ex)
            {
                _logger.LogWarning(ex, "Unable to collect city analytics of {ipAddress}", request.IPAddress);
            }
        }

        int year = request.RequestTime.Year;
        int month = request.RequestTime.Month;
        int day = request.RequestTime.Day;

        if (request.Errors is not null)
        {
            await _redirectionRequestAnalyticsRepository.AddDailyError(year, month, day, request.Host, request.Path, cityId);
            await _redirectionRequestAnalyticsRepository.AddMonthlyError(year, month, request.Host, request.Path, cityId);
            await _redirectionRequestAnalyticsRepository.AddYearlyError(year, request.Host, request.Path, cityId);
        }
        else
        {
            await _redirectionRequestAnalyticsRepository.AddDaily(year, month, day, request.Host, request.Path, cityId);
            await _redirectionRequestAnalyticsRepository.AddMonthly(year, month, request.Host, request.Path, cityId);
            await _redirectionRequestAnalyticsRepository.AddYearly(year, request.Host, request.Path, cityId);
        }
    }
}
