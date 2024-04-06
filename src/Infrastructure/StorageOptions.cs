using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TheGnouCommunity.UrlManager.Infrastructure;

public class StorageOptions
{
    public const string ConfigurationSectionName = nameof(StorageOptions);

    [NotNull]
    [Required]
    public string AccountName { get; set; } = default!;
    [NotNull]
    [Required]
    public string StorageAccountKey { get; set; } = default!;
}