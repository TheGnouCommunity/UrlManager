using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure;

public class TableStorageOptions
{
    public const string ConfigurationSectionName = nameof(TableStorageOptions);

    [NotNull]
    [Required]
    public string StorageUri { get; set; } = default!;
    [NotNull]
    [Required]
    public string AccountName { get; set; } = default!;
    [NotNull]
    [Required]
    public string StorageAccountKey { get; set; } = default!;
}