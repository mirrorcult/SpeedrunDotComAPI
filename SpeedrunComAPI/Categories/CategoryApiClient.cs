using System.Text.Json;
using JetBrains.Annotations;
using SpeedrunComAPI.Leaderboards;
using SpeedrunComAPI.Query;
using SpeedrunComAPI.Utility;
using SpeedrunComAPI.Variables;

namespace SpeedrunComAPI.Categories;

[PublicAPI]
public class CategoryApiClient
{
    public static Uri CategoryEndpointUri = new(SRCApiClient.EndpointUri, "categories/");
    
    private HttpClient _http;

    public CategoryApiClient(HttpClient http)
    {
        _http = http;
    }
    
    public async Task<CategoryModel> GetSingleCategory(string id, CategoryEmbedOptions embed=default)
    {
        var query = embed.GetEmbedQueryString<CategoryEmbedOptions>();
        var build = new UriBuilder(new Uri(CategoryEndpointUri, id))
        {
            Query = query
        };
        var str = await _http.GetStringAsync(build.Uri);
        var model = JsonSerializer.Deserialize<DataCategoryModel>(str, SRCApiClient.SerializerOptions);
        return model.Data;
    }
    
    // TODO GET /variables

    public async Task<VariableModel[]> GetSingleCategoryVariables(string id, CategoryVariableFilterOptions filter = default)
    {
        var query = filter.GetFilterQueryString<CategoryVariableFilterOptions>();
        var build = new UriBuilder(new Uri(new Uri(CategoryEndpointUri, id + "/"), "variables"))
        {
            Query = query
        };
        var str = await _http.GetStringAsync(build.Uri);
        var model = JsonSerializer.Deserialize<VariableListModel>(str, SRCApiClient.SerializerOptions);
        return model.Data;
    }    
    
    public async Task<LeaderboardModel[]> GetSingleCategoryRecords(string id, LeaderboardEmbedOptions embed=default, CategoryFilterOptions filter=default)
    {
        var query = embed.GetEmbedQueryString<LeaderboardEmbedOptions>();
        var filterQuery = filter.GetFilterQueryString<CategoryFilterOptions>();
        var build = new UriBuilder(new Uri(new Uri(CategoryEndpointUri, id + "/"), "records"))
        {
            Query = UtilityExt.ComposeQueryString(filterQuery, query)
        };
        var str = await _http.GetStringAsync(build.Uri);
        var model = JsonSerializer.Deserialize<LeaderboardListModel>(str, SRCApiClient.SerializerOptions);
        return model.Data;
    }
}