using System.Text.Json;
using JetBrains.Annotations;
using SpeedrunComAPI.Users;

namespace SpeedrunComAPI.Profile;

/// <summary>
///     Requires an API token to access.
/// </summary>
[PublicAPI]
public class ProfileApiClient
{
    public static Uri ProfileEndpointUri = new(SRCApiClient.EndpointUri, "profile");
    
    private HttpClient _http;

    public ProfileApiClient(HttpClient http)
    {
        _http = http;
    }

    /// <summary>
    ///     Retrieves the user model of the currently authenticated user.
    /// </summary>
    /// <remarks>
    ///     Only works if the API client has a valid API key.
    /// </remarks>
    public async Task<UserModel> GetCurrentProfile()
    {
        var str = await _http.GetStringAsync(ProfileEndpointUri);
        var model = JsonSerializer.Deserialize<DataUserModel>(str, SRCApiClient.SerializerOptions);
        return model.Data;
    }
}