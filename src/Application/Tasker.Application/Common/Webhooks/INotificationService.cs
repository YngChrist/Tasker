using Tasker.Application.Common.Webhooks.WebhookRequests;

namespace Tasker.Application.Common.Webhooks;

public interface INotificationService
{
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
    Task<bool> NotifyAsync<T>(string callbackUrl, T? payload = null) where T : BaseWebhookPayload;

    /// <summary>
    /// Выполняет callback запрос по указанному URL.
    /// </summary>
    /// <param name="callbackUrl">URL для выполнения GET-запроса.</param>
    /// <returns>Асинхронная задача, представляющая процесс выполнения уведомления. true - в случае успешного запроса и наоборот.</returns>
    /// <remarks>
    /// Метод отправляет GET-запрос на указанный URL. Если URL не указан, 
    /// в лог записывается предупреждение, и уведомление не отправляется.
    /// </remarks>
    Task<bool> NotifyAsync(string callbackUrl);
}