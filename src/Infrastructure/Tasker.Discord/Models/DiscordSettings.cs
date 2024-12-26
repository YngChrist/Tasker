namespace Tasker.Discord.Models;

/// <summary>
/// Настройки, связанные с дискордом.
/// </summary>
public class DiscordSettings
{
    /// <summary>
    /// Название настроек.
    /// </summary>
    public const string SectionName = "Discord";
    
    /// <summary>
    /// Токен Discord бота.
    /// </summary>
    public string Token { get; set; } = string.Empty;
}