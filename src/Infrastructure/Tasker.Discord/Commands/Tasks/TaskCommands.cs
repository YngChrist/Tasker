using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using Tasker.Application.Common.Model;

namespace Tasker.Discord.Commands.Tasks;

public class TaskCommands : ApplicationCommandModule
{
    private readonly TaskCommandExecutor _taskCommandExecutor;

    public TaskCommands(TaskCommandExecutor taskCommandExecutor)
    {
        _taskCommandExecutor = taskCommandExecutor;
    }

    [SlashCommand("task", "Создать задачу и обработать ее через pipeline.")]
    public async Task TaskCommandAsync(InteractionContext ctx, 
        [Option("Проект", "Проект к которому относится задача", true), Autocomplete(typeof(ProjectAutocompleteProvider))] string project,
        [Option("Статус", "К какому статусу приведет выполнение")] string statusText,
        [Option("Текст", "Текст задачи")] string taskText,
        [Option("Приоритет", "Приоритет задачи")] IssuePriority priority)
    {
        await ctx.DeferAsync();

        await _taskCommandExecutor.CreateTaskAsync(project, taskText, statusText, priority, builder => ctx.EditResponseAsync(builder));
    }
}