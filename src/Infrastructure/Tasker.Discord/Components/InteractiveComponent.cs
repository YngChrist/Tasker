using DSharpPlus;
using DSharpPlus.EventArgs;

namespace Tasker.Discord.Components;

/// <summary>
/// Базовый интерактивный компонент интерфейса.
/// </summary>
public class InteractiveComponent
{
    /// <summary>
    /// Идентификатор компонента.
    /// </summary>
    public string Id { get; protected init; }

    /// <summary>
    /// Действие при взаимодействии с компонентом.
    /// </summary>
    public Func<DiscordClient, ComponentInteractionCreateEventArgs, Task> Action { get; init; }

    /// <summary>
    /// Базовый интерактивный компонент интерфейса.
    /// </summary>
    public InteractiveComponent(string id, Func<DiscordClient, ComponentInteractionCreateEventArgs, Task> action)
    {
        Id = id;
        Action = action;
    }
}