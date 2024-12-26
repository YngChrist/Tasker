using DSharpPlus;
using DSharpPlus.Entities;

namespace Tasker.Discord.Components;

/// <summary>
/// Набор <see cref="InteractiveComponent"/> дял дальнейшего использования.
/// </summary>
public static class UiComponent
{
    /// <summary>
    /// Кнопка старта записи переписки.
    /// </summary>
    public static readonly DiscordComponent StartTextRecordingButton = 
        new DiscordButtonComponent(ButtonStyle.Secondary, Guid.NewGuid().ToString(), "Начать запись переписки");

    /// <summary>
    /// Кнопка остановки записи переписки.
    /// </summary>
    public static readonly DiscordComponent StopTextRecordingButton = 
        new DiscordButtonComponent(ButtonStyle.Secondary, Guid.NewGuid().ToString(), "Остановить запись переписки");

    /// <summary>
    /// Меню выбора сервисов-назначения для отправки данных.
    /// Поддерживает множественный выбор.
    /// Отправляет нажатие после того, как сделан выбор.
    /// </summary>
    public static readonly DiscordComponent DestinationServicesSelectMenu =
        new DiscordSelectComponent(
            customId: Guid.NewGuid().ToString(),
            options: new List<DiscordSelectComponentOption>
            {
                new("Confluence", "Confluence", "Отправка в Confluence"),
            },
            placeholder: "Выберите сервисы",
            minOptions: 1,
            maxOptions: 1);
}