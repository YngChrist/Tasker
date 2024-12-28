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
    public static readonly DiscordComponent SendTaskButton = 
        new DiscordButtonComponent(ButtonStyle.Secondary, Guid.NewGuid().ToString(), "Готово.");

    /// <summary>
    /// Кнопка старта записи переписки.
    /// </summary>
    public static readonly DiscordComponent NewTaskButton = 
        new DiscordButtonComponent(ButtonStyle.Secondary, Guid.NewGuid().ToString(), "Добавить задачу.");
    
    /// <summary>
    /// Меню выбора сервисов-назначения для отправки данных.
    /// Поддерживает множественный выбор.
    /// Отправляет нажатие после того, как сделан выбор.
    /// </summary>
    public static readonly DiscordComponent TaskPrioritySelectMenu =
        new DiscordSelectComponent(
            customId: Guid.NewGuid().ToString(),
            options: new List<DiscordSelectComponentOption>
            {
                new("Красная", "Красная", "Задача, которую нужно выполнить хоть чуть"),
                new("Желтая", "Желтая", "Задача, которую нужно выполнить хоть чуть"),
                new("Зеленая", "Зеленая", "Задача, которую нужно выполнить хоть чуть")
            },
            placeholder: "Выберите сервисы",
            minOptions: 1,
            maxOptions: 1);
    
    public static readonly DiscordComponent ProjectSelectMenu =
        new DiscordSelectComponent(
            customId: Guid.NewGuid().ToString(),
            options: new List<DiscordSelectComponentOption>
            {
                new("Valik", "Valik", "Задача, которую нужно выполнить хоть чуть"),
                new("AI-summary", "Желтая", "Задача, которую нужно выполнить хоть чуть"),
            },
            placeholder: "Выберите сервисы",
            minOptions: 1,
            maxOptions: 1);
}