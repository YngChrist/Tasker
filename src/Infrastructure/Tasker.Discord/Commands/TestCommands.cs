using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using Tasker.Refit;

namespace Tasker.Discord.Commands;

public class TestCommands : ApplicationCommandModule
{
    private readonly INotificationService _notificationService;

    public TestCommands(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [SlashCommand("testwebhook", "Тест хука")]
    public async Task TestWebhook(InteractionContext ctx)
    {
        await ctx.DeferAsync();
        var result = await _notificationService.Test(15);
        await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("Ping"));
    }
}