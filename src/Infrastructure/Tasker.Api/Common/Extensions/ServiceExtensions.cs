using Serilog;
using Tasker.Discord.Components;
using Tasker.Discord.Models;

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
        services.AddUiComponents();

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

            logger.LogDebug("Регистрация действий компонентов.");
            
            componentService.RegisterComponent(UiComponent.StartTextRecordingButton, async (client, args) =>
            {
                logger.LogInformation("Вызвано действие компонента {Component}", nameof(UiComponent.StartTextRecordingButton));
            });

            return componentService;
        });

        return services;
    }
}