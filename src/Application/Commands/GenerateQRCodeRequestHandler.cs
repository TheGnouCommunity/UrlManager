using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using Net.Codecrete.QrCodeGenerator;
using System.Text;

namespace TheGnouCommunity.UrlManager.Application.Commands;

internal sealed class GenerateQRCodeRequestHandler : IRequestHandler<GenerateQRCodeRequest, Result<byte[]>>
{
    private readonly ILogger _logger;

    public GenerateQRCodeRequestHandler(ILogger<GenerateQRCodeRequestHandler> logger)
    {
        ArgumentNullException.ThrowIfNull(logger);

        _logger = logger;
    }

    public async Task<Result<byte[]>> Handle(GenerateQRCodeRequest request, CancellationToken cancellationToken)
    {
        var qrCode = QrCode.EncodeText(request.Url, QrCode.Ecc.Medium);
        return Encoding.Default.GetBytes(qrCode.ToSvgString(4));
    }
}
