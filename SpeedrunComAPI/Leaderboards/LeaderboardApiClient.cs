using System.Text.Json;
using JetBrains.Annotations;
using SpeedrunComAPI.Query;
using SpeedrunComAPI.Utility;

namespace SpeedrunComAPI.Leaderboards;

[PublicAPI]
public class LeaderboardApiClient
{
    public Uri LeaderboardsApiEndpoint = new(SRCApiClient.EndpointUri, "leaderboards/");

    private HttpClient _http;

    public LeaderboardApiClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<LeaderboardModel> GetCategoryLeaderboard(string game, string category,
        LeaderboardFilterOptions filter = default, LeaderboardEmbedOptions embed = default)
    {
        var embedQuery = embed.GetEmbedQueryString<LeaderboardEmbedOptions>();
        var filterQuery = filter.GetFilterQueryString<LeaderboardFilterOptions>();
        var build = new UriBuilder(new Uri(new Uri(new Uri(LeaderboardsApiEndpoint, game + "/"), "category/"), category))
        {
            Query = UtilityExt.ComposeQueryString(filterQuery, embedQuery)
        };
        var str = await _http.GetStringAsync(build.Uri);
        return JsonSerializer.Deserialize<DataLeaderboardModel>(str, SRCApiClient.SerializerOptions).Data;
    }
    
    public async Task<LeaderboardModel> GetLevelLeaderboard(string game, string level, string category,
        LeaderboardFilterOptions filter = default, LeaderboardEmbedOptions embed = default)
    {
        var embedQuery = embed.GetEmbedQueryString<LeaderboardEmbedOptions>();
        var filterQuery = filter.GetFilterQueryString<LeaderboardFilterOptions>();
        var build = new UriBuilder(new Uri(new Uri(new Uri(new Uri(LeaderboardsApiEndpoint, game + "/"), "level/"), level + "/"), category))
        {
            Query = UtilityExt.ComposeQueryString(filterQuery, embedQuery)
        };
        var str = await _http.GetStringAsync(build.Uri);
        return JsonSerializer.Deserialize<DataLeaderboardModel>(str, SRCApiClient.SerializerOptions).Data;
    }
}