using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using Refit;
using Serilog;
using Tasker.Application.Common.Webhooks;
using Tasker.Discord.Commands.Tasks;
using Tasker.Discord.Components;
using Tasker.Discord.Models;
using Tasker.Discord.Responses;
using Tasker.Refit;

namespace Tasker.Api.Common.Extensions;

/// <summary>
/// Настройки сервисов веб приложения.
/// </summary>
public static class ServiceExtensions
{

    /// <summary>
    /// Регистрация сервисов приложения.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <returns>Обновленная коллекция сервисов.</returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddUiComponents()
            .AddSingleton<TaskCommandExecutor>();

        
        return services;
    }

    /// <summary>
    /// Настройка конфигурации.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <param name="configuration">Конфигурация.</param>
    /// <returns>Обновленная коллекция сервисов.</returns>
    public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DiscordSettings>(configuration.GetSection("Discord"));

        return services;
    }
    
    /// <summary>
    /// Настройка конфигурации.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <param name="configuration">Конфигурация.</param>
    /// <returns>Обновленная коллекция сервисов.</returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddScoped<INotificationService, NotificationService>()
            .AddRefitClient<INotifier>()
            .ConfigureHttpClient((provider, httpClient) =>
            {
                httpClient.BaseAddress = new Uri(configuration.GetConnectionString("n8n")!);
            });

        return services;
    }

    /// <summary>
    /// Регистрация сервисов веб-приложения.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <returns>Обновленная коллекция сервисов.</returns>
    public static IServiceCollection AddWeb(this IServiceCollection services)
    {
        services
            .AddControllers();

        services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen();

        return services;
    }

    /// <summary>
    /// Настройка логирования.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <param name="loggingBuilder">Система логирования.</param>
    /// <returns>Обновленная коллекция сервисов.</returns>
    public static IServiceCollection AddLogging(this IServiceCollection services, ILoggingBuilder loggingBuilder)
    {
        loggingBuilder.ClearProviders();

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();

        services.AddLogging(builder => builder.AddSerilog());

        return services;
    }

    /// <summary>
    /// Регистрация действий компонентов.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <returns>Обновленная коллекция сервисов.</returns>
    private static IServiceCollection AddUiComponents(this IServiceCollection services)
    {
        // Регистрация UI компонентов приложения, а также обработчика.
        services.AddSingleton<ComponentService>(provider =>
        {
            var logger = provider.GetRequiredService<ILogger<ComponentService>>();
            var componentService = new ComponentService();

            var taskService = provider.GetRequiredService<TaskCommandExecutor>();
            logger.LogDebug("Регистрация действий компонентов.");
            
            componentService.RegisterComponent(UiComponent.TaskPrioritySelectMenu, async (client, args) =>
            {
                logger.LogInformation("Вызвано действие компонента {Component}", nameof(UiComponent.TaskPrioritySelectMenu));

                var sessionIdString = await GetField("Id:", args);
                logger.LogInformation("Поле Id получено {IdValue}.", sessionIdString);

                var value = args.Values.First();
            });
            
            
            return componentService;
        });

        return services;
    }

    private static async Task<string> GetField(string fieldName, ComponentInteractionCreateEventArgs args)
    {
        var embed = args.Message.Embeds.FirstOrDefault();
        if (embed == null)
        {
            await args.Interaction.EditOriginalResponseAsync(new DiscordWebhookBuilder()
                .AddEmbed(Embed.Error("Ошибка при выполнении.")));

            throw new Exception();
        }

        var idField = embed.Fields.FirstOrDefault(field => field.Name == fieldName);
        if (idField == null)
        {
            await args.Interaction.EditOriginalResponseAsync(new DiscordWebhookBuilder()
                .AddEmbed(Embed.Error("Ошибка при выполнении.")));

            throw new Exception();
        }

        return idField.Value.Replace("```", "").Trim();
    }
}