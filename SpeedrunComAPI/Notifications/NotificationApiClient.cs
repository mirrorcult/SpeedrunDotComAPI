using System.Text.Json;
using JetBrains.Annotations;
using SpeedrunComAPI.Query;

namespace SpeedrunComAPI.Notifications;

/// <summary>
///     Requires an API token to access.
/// </summary>
[PublicAPI]
public class NotificationApiClient
{
    public static Uri NotificationEndpointUri = new(SRCApiClient.EndpointUri, "notifications");
    
    private HttpClient _http;

    public NotificationApiClient(HttpClient http)
    {
        _http = http;
    }

    /// <summary>
    ///     Retrieves the notifications for the currently authenticated user.
    /// </summary>
    /// <remarks>
    ///     Only works if the API client has a valid API key.
    /// </remarks>
    public async Task<NotificationModel[]> GetCurrentNotifications(NotificationFilterOptions filter = default)
    {
        var build = new UriBuilder(NotificationEndpointUri)
        {
            Query = filter.GetFilterQueryString<NotificationFilterOptions>()
        };
        var str = await _http.GetStringAsync(build.Uri);
        var model = JsonSerializer.Deserialize<NotificationListModel>(str, SRCApiClient.SerializerOptions);
        return model.Data;
    }
}