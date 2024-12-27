using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using Tasker.Application.Common.Webhooks;

namespace Tasker.Discord.Commands;

public class TestCommands : ApplicationCommandModule
{
    private readonly INotificationService _notifier;

    public TestCommands(INotificationService notifier)
    {
        _notifier = notifier;
    }

    [SlashCommand("testwebhook", "Тест хука")]
    public async Task TestWebhook(InteractionContext ctx)
    {
        await ctx.DeferAsync();
        await _notifier.NotifyAsync("ping");
        await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("Ping"));
    }
    
    [SlashCommand("pdftest", "Тест хука")]
    public async Task PdfTest(InteractionContext ctx)
    {
        await ctx.DeferAsync();
        var path = "../../test.pdf";
        new PdfReportGenerator().GenerateReport(path);
        
        await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddFile(File.OpenRead(path)));
    }
}