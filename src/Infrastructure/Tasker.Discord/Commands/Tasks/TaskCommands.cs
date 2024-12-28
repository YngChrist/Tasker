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
        [Option("project", "ads")] string project,
        [Option("статус", "К какому статусу приведет выполнение")] string statusText,
        [Option("текст", "текст задачи")] string taskText,
        [Option("priority", "ads")] IssuePriority priority)
    {
        await ctx.DeferAsync();

        await _taskCommandExecutor.CreateTaskAsync(project, taskText, statusText, priority, builder => ctx.EditResponseAsync(builder));
    }
}