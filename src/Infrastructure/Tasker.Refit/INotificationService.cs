using Refit;

namespace Tasker.Refit;

public interface INotificationService
{
    [Get("/webhook/ping")]
    Task<IApiResponse> Test([Body] int interval);
}