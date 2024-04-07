using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TheGnouCommunity.UrlManager.Application.Commands;

public class GeoIP2Options
{
    public const string ConfigurationSectionName = nameof(GeoIP2Options);

    [Required]
    public int AccountId { get; set; } = default;

    [NotNull]
    [Required]
    public string LicenseKey { get; set; } = null!;
}