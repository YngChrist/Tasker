using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace Tasker.Discord.Commands.Tasks;

public class ProjectAutocompleteProvider : IAutocompleteProvider
{
    // Список доступных проектов
    private static readonly List<string> Projects = new()
    {
        "Проект A",
        "Проект B",
        "Проект C",
        "Проект D"
    };

    public Task<IEnumerable<DiscordAutoCompleteChoice>> Provider(AutocompleteContext ctx)
    {
        // Фильтруем список проектов на основе ввода пользователя
        var userInput = ctx.OptionValue?.ToString() ?? string.Empty;
        return Task.FromResult(Projects
            .Where(project => project.Contains(userInput, StringComparison.OrdinalIgnoreCase))
            .Select(project => new DiscordAutoCompleteChoice(project, project)));
    }
}