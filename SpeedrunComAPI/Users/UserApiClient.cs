using System.Text.Json;
using JetBrains.Annotations;
using SpeedrunComAPI.Query;
using SpeedrunComAPI.Runs;

namespace SpeedrunComAPI.Users;

[PublicAPI]
public class UserApiClient
{
    public static Uri AllUsersEndpointUri = new(SRCApiClient.EndpointUri, "users");
    public static Uri UserEndpointUri = new(SRCApiClient.EndpointUri, "users/");
    
    private HttpClient _http;

    public UserApiClient(HttpClient http)
    {
        _http = http;
    }
    
    public async Task<UserModel> GetSingleUser(string id)
    {
        var str = await _http.GetStringAsync(new Uri(UserEndpointUri, id));
        var model = JsonSerializer.Deserialize<DataUserModel>(str, SRCApiClient.SerializerOptions);
        return model.Data;
    }

    public async Task<UserModel[]> GetAllUsers(UserFilterOptions options)
    {
        var query = options.GetFilterQueryString<UserFilterOptions>();
        var build = new UriBuilder(AllUsersEndpointUri)
        {
            Query = query
        };
        var str = await _http.GetStringAsync(build.Uri);
        var model = JsonSerializer.Deserialize<UserListModel>(str, SRCApiClient.SerializerOptions);
        return model.Data;
    }
    
    public async Task<RunPlaceModel[]> GetPersonalBests(string id, RunEmbedOptions embed = default)
    {        
        var query = embed.GetEmbedQueryString<RunEmbedOptions>();
        var build = new UriBuilder(new Uri(UserEndpointUri, $"{id}/personal-bests"))
        {
            Query = query
        };
        var str = await _http.GetStringAsync(build.Uri);
        var model = JsonSerializer.Deserialize<RunPlaceListModel>(str, SRCApiClient.SerializerOptions);
        return model.Data;
    }
}