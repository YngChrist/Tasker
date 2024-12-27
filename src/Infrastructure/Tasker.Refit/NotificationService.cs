using Ardalis.GuardClauses;
using Microsoft.Extensions.Logging;
using Tasker.Application.Common.Webhooks;
using Tasker.Application.Common.Webhooks.WebhookRequests;

namespace Tasker.Refit;

public class NotificationService : INotificationService
{
    private readonly INotifier _notifier;
    private readonly ILogger<NotificationService> _logger;

    public NotificationService(INotifier notifier, ILogger<NotificationService> logger)
    {
        _notifier = notifier;
        _logger = logger;
    }

    /// <summary>
    /// Выполняет callback запрос по указанному URL.
    /// </summary>
    /// <param name="callbackUrl">URL для выполнения GET-запроса.</param>
    /// <param name="payload">Отправляемое сообщение.</param>
    /// <returns>Асинхронная задача, представляющая процесс выполнения уведомления. true - в случае успешного запроса и наоборот.</returns>
    /// <remarks>
    /// Метод отправляет GET-запрос на указанный URL. Если URL не указан, 
    /// в лог записывается предупреждение, и уведомление не отправляется.
    /// </remarks>
    public async Task<bool> NotifyAsync<T>(string callbackUrl, T? payload = null) where T : BaseWebhookPayload
    {
        Guard.Against.NullOrWhiteSpace(callbackUrl, nameof(callbackUrl));
        Guard.Against.Null(payload, nameof(payload));
        
        try
        {
            _logger.LogInformation("Converted payload - {Payload}.", System.Text.Json.JsonSerializer.Serialize(payload));

            _logger.LogInformation("Отправка callback на {CallbackUrl} с телом {Payload}.", callbackUrl, payload);

            var response = await _notifier.Webhook(callbackUrl, payload);

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Успешно отправлен callback на {CallbackUrl}.", callbackUrl);
                return true;
            }

            _logger.LogWarning("Ошибка отправки callback на {CallbackUrl}: {StatusCode}.", callbackUrl,
                response.StatusCode);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка выполнения callback запроса на {CallbackUrl}.", callbackUrl);
        }

        return false;
    }

    /// <summary>
    /// Выполняет callback запрос по указанному URL.
    /// </summary>
    /// <param name="callbackUrl">URL для выполнения GET-запроса.</param>
    /// <returns>Асинхронная задача, представляющая процесс выполнения уведомления. true - в случае успешного запроса и наоборот.</returns>
    /// <remarks>
    /// Метод отправляет GET-запрос на указанный URL. Если URL не указан, 
    /// в лог записывается предупреждение, и уведомление не отправляется.
    /// </remarks>
    public async Task<bool> NotifyAsync(string callbackUrl)
    {
        Guard.Against.NullOrWhiteSpace(callbackUrl, nameof(callbackUrl));
        
        try
        {
            _logger.LogInformation("Отправка callback на {CallbackUrl}.", callbackUrl);

            var response = await _notifier.Webhook(callbackUrl);

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Успешно отправлен callback на {CallbackUrl}.", callbackUrl);
                return true;
            }

            _logger.LogWarning("Ошибка отправки callback на {CallbackUrl}: {StatusCode}.", callbackUrl,
                response.StatusCode);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка выполнения callback запроса на {CallbackUrl}.", callbackUrl);
        }

        return false;
    }
}