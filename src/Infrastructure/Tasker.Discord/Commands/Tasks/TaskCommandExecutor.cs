using DSharpPlus.Entities;
using Tasker.Application.Common.Model;
using Tasker.Discord.Components;
using Tasker.Discord.Responses;

namespace Tasker.Discord.Commands.Tasks;

public class TaskCommandExecutor
{
    public async Task CreateTaskAsync(string project, string taskText, string statusText, IssuePriority priority, Func<DiscordWebhookBuilder, Task> editResponseFunc)
    {
        var random = new Random();
        var id = (ulong) random.NextInt64();

        var task = new Issue
        {
            Id = id,
            TaskText = taskText,
            StatusText = statusText,
            Priority = priority,
            Project = project
        };

        await editResponseFunc(new DiscordWebhookBuilder()
            .AddEmbed(Embed.TaskInfo(task)));
    }
}