using System.Text.Json;
using JetBrains.Annotations;
using SpeedrunComAPI.Categories;
using SpeedrunComAPI.Leaderboards;
using SpeedrunComAPI.Levels;
using SpeedrunComAPI.Query;
using SpeedrunComAPI.Utility;
using SpeedrunComAPI.Variables;

namespace SpeedrunComAPI.Games;

[PublicAPI]
public class GameApiClient
{
    public static Uri AllGamesEndpointUri = new(SRCApiClient.EndpointUri, "games");
    public static Uri GameEndpointUri = new(SRCApiClient.EndpointUri, "games/");
    
    private HttpClient _http;

    public GameApiClient(HttpClient http)
    {
        _http = http;
    }
    
    public async Task<GameModel> GetSingleGame(string id, GameEmbedOptions embed = default)
    {
        var query = embed.GetEmbedQueryString<GameEmbedOptions>();
        var build = new UriBuilder(new Uri(GameEndpointUri, id))
        {
            Query = query
        };
        var str = await _http.GetStringAsync(build.Uri);
        var model = JsonSerializer.Deserialize<DataGameModel>(str, SRCApiClient.SerializerOptions);
        return model.Data;
    }

    public async Task<GameModel[]> GetAllGames(GameFilterOptions options, GameEmbedOptions embed = default)
    {
        var query = options.GetFilterQueryString<GameFilterOptions>();
        var embedStr = embed.GetEmbedQueryString<GameEmbedOptions>();
        var build = new UriBuilder(AllGamesEndpointUri)
        {
            Query = UtilityExt.ComposeQueryString(query, embedStr)
        };
        var str = await _http.GetStringAsync(build.Uri);
        var model = JsonSerializer.Deserialize<GameListModel>(str, SRCApiClient.SerializerOptions);
        return model.Data;
    }
    
    public async Task<CategoryModel[]> GetSingleGameCategories(string id, GameCategoryFilterOptions filter = default)
    {
        var query = filter.GetFilterQueryString<GameCategoryFilterOptions>();
        var build = new UriBuilder(new Uri(new Uri(GameEndpointUri, id + "/"), "categories"))
        {
            Query = query
        };
        var str = await _http.GetStringAsync(build.Uri);
        var model = JsonSerializer.Deserialize<CategoryListModel>(str, SRCApiClient.SerializerOptions);
        return model.Data;
    }
    
    public async Task<LevelModel[]> GetSingleGameLevels(string id, GameGenericFilterOptions filter = default)
    {
        var query = filter.GetFilterQueryString<GameGenericFilterOptions>();
        var build = new UriBuilder(new Uri(new Uri(GameEndpointUri, id + "/"), "levels"))
        {
            Query = query
        };
        var str = await _http.GetStringAsync(build.Uri);
        var model = JsonSerializer.Deserialize<LevelListModel>(str, SRCApiClient.SerializerOptions);
        return model.Data;
    }
    
    public async Task<VariableModel[]> GetSingleGameVariables(string id, GameGenericFilterOptions filter = default)
    {
        var query = filter.GetFilterQueryString<GameGenericFilterOptions>();
        var build = new UriBuilder(new Uri(new Uri(GameEndpointUri, id + "/"), "variables"))
        {
            Query = query
        };
        var str = await _http.GetStringAsync(build.Uri);
        var model = JsonSerializer.Deserialize<VariableListModel>(str, SRCApiClient.SerializerOptions);
        return model.Data;
    }
    
    public async Task<GameModel[]> GetSingleGameDerivedGames(string id, GameFilterOptions filter = default)
    {
        var query = filter.GetFilterQueryString<GameFilterOptions>();
        var build = new UriBuilder(new Uri(new Uri(GameEndpointUri, id + "/"), "derived-games"))
        {
            Query = query
        };
        var str = await _http.GetStringAsync(build.Uri);
        var model = JsonSerializer.Deserialize<GameListModel>(str, SRCApiClient.SerializerOptions);
        return model.Data;
    } 
    
    public async Task<LeaderboardModel[]> GetSingleGameRecords(string id, GameRecordsFilterOptions filter = default, LeaderboardEmbedOptions embed = default)
    {
        var query = filter.GetFilterQueryString<GameRecordsFilterOptions>();
        var embedQuery = embed.GetEmbedQueryString<LeaderboardEmbedOptions>();
        var build = new UriBuilder(new Uri(new Uri(GameEndpointUri, id + "/"), "records"))
        {
            Query = UtilityExt.ComposeQueryString(query, embedQuery)
        };
        var str = await _http.GetStringAsync(build.Uri);
        var model = JsonSerializer.Deserialize<LeaderboardListModel>(str, SRCApiClient.SerializerOptions);
        return model.Data;
    } 
}