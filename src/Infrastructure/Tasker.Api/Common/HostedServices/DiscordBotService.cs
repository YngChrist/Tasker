using DSharpPlus;

namespace Tasker.Api.Common.HostedServices;

/// <summary>
/// Сервис отвечающий за включение бота.
/// </summary>
public class DiscordBotService : IHostedService
{
    private readonly DiscordClient _discordClient;
    private readonly ILogger<DiscordBotService> _logger;

    /// <summary>
    /// Сервис отвечающий за включение бота.
    /// </summary>
    public DiscordBotService(DiscordClient discordClient, ILogger<DiscordBotService> logger)
    {
        _discordClient = discordClient;
        _logger = logger;
    }

    /// <summary>
    /// Подключение бота.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запуск Discord бота...");
        await _discordClient.ConnectAsync();
        _logger.LogInformation("Бот запущен.");
    }

    /// <summary>
    /// Завершение работы бота.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Завершение работы Discord бота...");
        await _discordClient.DisconnectAsync();
        _logger.LogInformation("Бот выключен.");
    }
}