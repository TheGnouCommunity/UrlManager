using FluentResults;
using MediatR;

namespace TheGnouCommunity.UrlManager.Application.Commands;

public sealed class GenerateQRCodeRequest : IRequest<Result<byte[]>>
{
    public string Url { get; }

    public GenerateQRCodeRequest(string url)
    {
        ArgumentNullException.ThrowIfNull(url);

        Url = url;
    }
}