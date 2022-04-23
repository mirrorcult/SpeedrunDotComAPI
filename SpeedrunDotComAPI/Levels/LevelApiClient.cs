using System.Text.Json;
using JetBrains.Annotations;
using SpeedrunDotComAPI.Query;
using SpeedrunDotComAPI.Categories;
using SpeedrunDotComAPI.Leaderboards;
using SpeedrunDotComAPI.Utility;
using SpeedrunDotComAPI.Variables;

namespace SpeedrunDotComAPI.Levels;

[PublicAPI]
public class LevelApiClient
{
    public static Uri LevelEndpointUri = new(SRCApiClient.EndpointUri, "levels/");
    
    private HttpClient _http;

    public LevelApiClient(HttpClient http)
    {
        _http = http;
    }
    
    public async Task<LevelModel> GetSingleLevel(string id, LevelEmbedOptions embed=default)
    {
        var query = embed.GetEmbedQueryString<LevelEmbedOptions>();
        var build = new UriBuilder(new Uri(LevelEndpointUri, id))
        {
            Query = query
        };
        var str = await _http.GetStringAsync(build.Uri);
        var model = JsonSerializer.Deserialize<DataLevelModel>(str, SRCApiClient.SerializerOptions);
        return model.Data;
    }
    
    public async Task<VariableModel[]> GetSingleLevelCategories(string id, LevelVariablesFilterOptions filter=default)
    {
        var query = filter.GetFilterQueryString<LevelVariablesFilterOptions>();
        var build = new UriBuilder(new Uri(new Uri(LevelEndpointUri, id + "/"), "variables"))
        {
            Query = query
        };
        var str = await _http.GetStringAsync(build.Uri);
        var model = JsonSerializer.Deserialize<VariableListModel>(str, SRCApiClient.SerializerOptions);
        return model.Data;
    }
    
    public async Task<CategoryModel[]> GetSingleLevelCategories(string id, CategoryEmbedOptions embed=default)
    {
        var query = embed.GetEmbedQueryString<CategoryEmbedOptions>();
        var build = new UriBuilder(new Uri(new Uri(LevelEndpointUri, id + "/"), "categories"))
        {
            Query = query
        };
        var str = await _http.GetStringAsync(build.Uri);
        var model = JsonSerializer.Deserialize<CategoryListModel>(str, SRCApiClient.SerializerOptions);
        return model.Data;
    }
    
    public async Task<LeaderboardModel[]> GetSingleLevelRecords(string id, LevelRecordsFilterOptions filter=default, LeaderboardEmbedOptions embed=default)
    {
        var filterQuery = filter.GetFilterQueryString<LevelRecordsFilterOptions>();
        var query = embed.GetEmbedQueryString<LeaderboardEmbedOptions>();
        var build = new UriBuilder(new Uri(new Uri(LevelEndpointUri, id + "/"), "records"))
        {
            Query = UtilityExt.ComposeQueryString(filterQuery, query)
        };
        var str = await _http.GetStringAsync(build.Uri);
        var model = JsonSerializer.Deserialize<LeaderboardListModel>(str, SRCApiClient.SerializerOptions);
        return model.Data;
    }
}