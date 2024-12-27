using Refit;
using Tasker.Application.Common.Webhooks.WebhookRequests;

namespace Tasker.Refit;

public interface INotifier
{
    /// <summary>
    /// Отправка GET запроса на вебхук.
    /// </summary>
    /// <param name="path">Путь к вебхуку.</param>
    /// <param name="message">Сообщение.</param>
    [Get("/webhook/{path}")]
    Task<IApiResponse> Webhook<T>(string path, [Body] T message) where T : BaseWebhookPayload;

    /// <summary>
    /// Отправка GET запроса на вебхук.
    /// </summary>
    /// <param name="path">Путь к вебхуку.</param>
    [Get("/webhook/{path}")]
    Task<IApiResponse> Webhook(string path);
}