using System.Dynamic;
using System.Text.Json;
using JetBrains.Annotations;
using SpeedrunDotComAPI.Categories;
using SpeedrunDotComAPI.Games;
using SpeedrunDotComAPI.Leaderboards;
using SpeedrunDotComAPI.Levels;
using SpeedrunDotComAPI.Metadata;
using SpeedrunDotComAPI.Notifications;
using SpeedrunDotComAPI.Profile;
using SpeedrunDotComAPI.Runs;
using SpeedrunDotComAPI.Series;
using SpeedrunDotComAPI.Users;
using SpeedrunDotComAPI.Utility;
using SpeedrunDotComAPI.Variables;

namespace SpeedrunDotComAPI;

[PublicAPI]
public class SRCApiClient
{
    public static Uri BaseUri = new("https://www.speedrun.com/");
    public static Uri EndpointUri = new(BaseUri, "api/v1/"); 
    
    internal static JsonSerializerOptions SerializerOptions = new() { PropertyNamingPolicy = new KebabCaseNamingPolicy() };
    private HttpClient _http;

    #region API sub-clients

    public GameApiClient Games;
    public UserApiClient Users;
    public RunApiClient Runs;
    public LeaderboardApiClient Leaderboards;
    public CategoryApiClient Categories;
    public LevelApiClient Levels;
    public MetadataApiClient Metadata;
    public VariableApiClient Variables;
    public SeriesApiClient Series;
    public ProfileApiClient Profile;
    public NotificationApiClient Notifications;
    
    #endregion
    
    public SRCApiClient(string? apiKey=null)
    {
        _http = new();
        if (apiKey != null)
            _http.DefaultRequestHeaders.Add("X-API-Key", apiKey);
        _http.DefaultRequestHeaders.Add("User-Agent", "SpeedrunDotComAPI/1.0");
        Games = new(_http);
        Users = new(_http);
        Runs = new(_http);
        Leaderboards = new(_http);
        Categories = new(_http);
        Levels = new(_http);
        Metadata = new(_http);
        Variables = new(_http);
        Series = new(_http);
        Profile = new(_http);
        Notifications = new(_http);
    }

    /// <summary>
    ///     If you really need this for some reason here you go.
    ///     Extreme low-level API.
    /// </summary>
    public async Task<dynamic?> RawDataQuery(string query)
    {
        return JsonSerializer.Deserialize<ExpandoObject>(await _http.GetStringAsync(new Uri(EndpointUri, query)));
    }
}