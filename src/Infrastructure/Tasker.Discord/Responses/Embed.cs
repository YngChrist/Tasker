using System.Text;
using DSharpPlus.Entities;

namespace Tasker.Discord.Responses;

/// <summary>
/// Вспомогательный класс для выстраивания ответов.
/// </summary>
public static class Embed
{
    /// <summary>
    /// Необходим для отправки пустой разделительной строки в описании, так как 
    /// Дискорд делает .Trim() для строки перед отправкой. 
    /// </summary>
    private const string EmptySymbol = "ㅤ";

    private const string ErrorLiteral = "Ошибка.";

    /// <summary>
    /// Создание ответа - ошибки.
    /// </summary>
    /// <param name="description">Информация.</param>
    /// <returns>Embed для отправки его в ответ.</returns>
    public static DiscordEmbed Error(string description)
    {
        return new DiscordEmbedBuilder()
            .WithDescription($"""
                              ### {ErrorLiteral}
                              **{description}**
                              """)
            .WithColor(DiscordColor.IndianRed)
            .Build();
    }

    /// <summary>
    /// Создание ответа - ошибки.
    /// </summary>
    /// <param name="title">Заголовок ошибки.</param>
    /// <param name="description">Описание ошибки.</param>
    /// <returns>Embed для отправки его в ответ.</returns>
    public static DiscordEmbed Error(string title, string description)
    {
        return new DiscordEmbedBuilder()
            .WithDescription($"""
                              ## {ErrorLiteral}
                              ### {title} 
                               {description}
                               {EmptySymbol}
                              """)
            .WithColor(DiscordColor.IndianRed)
            .Build();
    }

    /// <summary>
    /// Создание ответа, содержащего информацию.
    /// </summary>
    /// <param name="title">Заголовок.</param>
    /// <param name="description">Информация.</param>
    /// <returns>Embed для отправки его в ответ.</returns>
    public static DiscordEmbed Info(string title, string description)
    {
        return new DiscordEmbedBuilder()
            .WithTitle(title)
            .WithDescription(description)
            .WithColor(DiscordColor.Cyan)
            .Build();
    }

    /// <summary>
    /// Создание ответа с информацией о кнопках.
    /// </summary>
    /// <returns>Embed для отправки в ответ.</returns>
    public static DiscordEmbed ButtonMenuInfo()
    {
        return new DiscordEmbedBuilder()
            .WithDescription("### Для начала можете воспользоваться следующими кнопками: ")
            .WithColor(DiscordColor.Green)
            .Build();
    }
}