using System.Text.Json;
using JetBrains.Annotations;
using SpeedrunComAPI.Games;
using SpeedrunComAPI.Query;
using SpeedrunComAPI.Utility;

namespace SpeedrunComAPI.Series;

[PublicAPI]
public class SeriesApiClient
{
    public static Uri AllSeriesEndpointUri = new(SRCApiClient.EndpointUri, "series");
    public static Uri SeriesEndpointUri = new(SRCApiClient.EndpointUri, "series/");
    
    private HttpClient _http;

    public SeriesApiClient(HttpClient http)
    {
        _http = http;
    }
    
    public async Task<SeriesModel> GetSingleSeries(string id, SeriesEmbedOptions embed = default)
    {
        var query = embed.GetEmbedQueryString<SeriesEmbedOptions>();
        var build = new UriBuilder(new Uri(SeriesEndpointUri, id))
        {
            Query = query
        };
        var str = await _http.GetStringAsync(build.Uri);
        var model = JsonSerializer.Deserialize<DataSeriesModel>(str, SRCApiClient.SerializerOptions);
        return model.Data;
    }

    public async Task<SeriesModel[]> GetAllSeries(SeriesFilterOptions options, SeriesEmbedOptions embed = default)
    {
        var query = options.GetFilterQueryString<SeriesFilterOptions>();
        var embedStr = embed.GetEmbedQueryString<SeriesEmbedOptions>();
        var build = new UriBuilder(AllSeriesEndpointUri)
        {
            Query = UtilityExt.ComposeQueryString(query, embedStr)
        };
        var str = await _http.GetStringAsync(build.Uri);
        var model = JsonSerializer.Deserialize<SeriesListModel>(str, SRCApiClient.SerializerOptions);
        return model.Data;
    }
    
    public async Task<GameModel[]> GetSeriesGames(string id, GameFilterOptions filter = default, SeriesEmbedOptions embed = default)
    {
        var query = filter.GetFilterQueryString<GameFilterOptions>();
        var embedStr = embed.GetEmbedQueryString<SeriesEmbedOptions>();
        var build = new UriBuilder(new Uri(new Uri(SeriesEndpointUri, id + "/"), "games"))
        {
            Query = UtilityExt.ComposeQueryString(query, embedStr)
        };
        var str = await _http.GetStringAsync(build.Uri);
        var model = JsonSerializer.Deserialize<GameListModel>(str, SRCApiClient.SerializerOptions);
        return model.Data;
    }
}