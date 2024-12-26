using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;

namespace Tasker.Discord.Components;

/// <summary>
/// Сервис, отвечающий за работу с UI компонентами.
/// </summary>
public class ComponentService
{
    private readonly List<InteractiveComponent> _buttons;

    /// <summary>
    /// Сервис, отвечающий за работу с UI компонентами.
    /// </summary>
    public ComponentService()
    {
        _buttons = [];
    }

    /// <summary>
    /// Регистрация действий для компонентов.
    /// </summary>
    /// <param name="component"></param>
    /// <param name="callback"></param>
    public void RegisterComponent(DiscordComponent component, Func<DiscordClient, ComponentInteractionCreateEventArgs, Task> callback)
    {
        var interactiveComponent = new InteractiveComponent(component.CustomId, callback);
        _buttons.Add(interactiveComponent);
    }

    /// <summary>
    /// Поиск и выполнение действий конкретного компонента.
    /// </summary>
    /// <remarks>
    /// В Discord у компонентов есть внутренний Id, по которому можно идентифицировать каждый компонент.
    /// Внутренний Id соответствует тому, что находится в InteractiveComponent.Id.
    /// </remarks>
    /// <param name="client">Клиент discord.</param>
    /// <param name="eventArgs">Информация о событии.</param>
    public async Task Execute(DiscordClient client, ComponentInteractionCreateEventArgs eventArgs)
    {
        var componentId = eventArgs.Interaction.Data.CustomId;

        var component = _buttons.FirstOrDefault(x => x.Id == componentId);
        
        if (component == null) return;
        
        await component.Action.Invoke(client, eventArgs);
    }
}