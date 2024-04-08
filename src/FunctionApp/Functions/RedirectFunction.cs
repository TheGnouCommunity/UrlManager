using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using TheGnouCommunity.UrlManager.Application.Commands;

namespace FunctionApp.Functions;

public sealed class RedirectFunction
{
    private readonly ILogger _logger;
    private readonly IMediator _mediator;

    public RedirectFunction(ILoggerFactory loggerFactory, IMediator mediator)
    {
        _logger = loggerFactory.CreateLogger<RedirectFunction>();
        _mediator = mediator;
    }

    [Function(nameof(RedirectFunction))]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "{**catchAll}")] HttpRequest httpRequest,
        string catchAll)
    {
        if (string.IsNullOrWhiteSpace(catchAll))
        {
            return new BadRequestResult();
        }

        if (httpRequest.QueryString.HasValue)
        {
            if (!httpRequest.Query.ContainsKey("qr"))
            {
                return new BadRequestResult();
            }

            var generateQRCodeRequest = new GenerateQRCodeRequest(UriHelper.BuildAbsolute(httpRequest.Scheme, httpRequest.Host, path: httpRequest.Path));
            _logger.LogInformation("Start processing QR code request for {url}.", generateQRCodeRequest.Url);

            var generateQRCodeRequestResult = await _mediator.Send(generateQRCodeRequest);
            if (generateQRCodeRequestResult.IsFailed)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return new FileContentResult(generateQRCodeRequestResult.Value, "image/svg+xml");
        }

        string? ipAddress = httpRequest.HttpContext.Connection.RemoteIpAddress?.ToString();
        if (httpRequest.Headers.TryGetValue("CLIENT-IP", out var headerValue))
        {
            ipAddress = headerValue.FirstOrDefault();
            if (ipAddress is not null)
            {
                ipAddress = ipAddress.Split(":")[0];
            }
        }

        var redirectionRequest = new RedirectionRequest(httpRequest.Host.Host, UrlEncoder.Default.Encode(catchAll), ipAddress);
        _logger.LogInformation("Start processing Redirect request for {host}/{path}.", redirectionRequest.Host, redirectionRequest.Path);

        var redirectionRequestResult = await _mediator.Send(redirectionRequest);
        if (redirectionRequestResult.IsFailed)
        {
            _logger.LogWarning("No target URL found for redirection request {host}/{path}.", redirectionRequest.Host, redirectionRequest.Path);
            return new NotFoundResult();
        }

        string targetUrl = redirectionRequestResult.Value;

        _logger.LogInformation("Found target URL {targeUrl} for {host}/{path}.", targetUrl, redirectionRequest.Host, redirectionRequest.Path);

        return new RedirectResult(targetUrl);
    }
}
