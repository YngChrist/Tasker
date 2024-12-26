using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tasker.Discord.Commands;
using Tasker.Discord.Components;
using Tasker.Discord.Models;
using Tasker.Discord.Responses;
using Tasker.Refit;

namespace Tasker.Discord;

public static class DiscordClientFactory
{
    public static IServiceCollection AddDiscordClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<DiscordClient>(provider =>
        {
            // Получение логгера для Program
            var logger = provider.GetRequiredService<ILogger<DiscordClient>>();

            // Получение и логгирование DiscordSettings
            var discordSettings = configuration.GetSection("Discord").Get<DiscordSettings>();

            if (discordSettings == null)
            {
                logger.LogCritical("Ошибка: настройки Discord не заданы.");

                throw new ArgumentNullException(nameof(discordSettings));
            }

            if (string.IsNullOrWhiteSpace(discordSettings.Token))
            {
                logger.LogCritical("Ошибка: токен Discord не задан в конфигурации.");
                throw new ArgumentNullException(nameof(discordSettings.Token));
            }

            logger.LogInformation("Используемые настройки Discord:");
            logger.LogInformation("Token: {Token}", discordSettings.Token);

            var discordConfig = new DiscordConfiguration
            {
                Token = discordSettings.Token,
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.All,
                LoggerFactory = provider.GetRequiredService<ILoggerFactory>(),
                HttpTimeout = TimeSpan.FromSeconds(30)
            };

            var client = new DiscordClient(discordConfig);

            var slashCommands = client.UseSlashCommands(new SlashCommandsConfiguration
            {
                Services = provider
            });

            slashCommands.RegisterCommands<TestCommands>();

            // Обработка взаимодействия с компонентом
            client.ComponentInteractionCreated += async (discordClient, args) =>
            {
                await args.Interaction.CreateResponseAsync(InteractionResponseType.DeferredMessageUpdate);

                await args.Interaction.EditOriginalResponseAsync(new DiscordWebhookBuilder()
                    .AddEmbed(Embed.Info("Обработка команды...", "Сейчас закончим!")));

                var componentService = provider.GetRequiredService<ComponentService>();
                await componentService.Execute(discordClient, args);
            };

            return client;
        });

        return services;
    }
}