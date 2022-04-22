using System.Text.Json;
using JetBrains.Annotations;
using SpeedrunComAPI.Query;

namespace SpeedrunComAPI.Runs;

[PublicAPI]
public class RunApiClient
{
    public static Uri AllRunsEndpointUri = new(SRCApiClient.EndpointUri, "runs");
    public static Uri RunEndpointUri = new(SRCApiClient.EndpointUri, "runs/");
    
    private HttpClient _http;

    public RunApiClient(HttpClient http)
    {
        _http = http;
    }
    
    public async Task<RunModel> GetSingleRun(string id, RunEmbedOptions embed = default)
    {
        var query = embed.GetEmbedQueryString<RunEmbedOptions>();
        var build = new UriBuilder(new Uri(RunEndpointUri, id))
        {
            Query = query
        };
        var str = await _http.GetStringAsync(build.Uri);
        var model = JsonSerializer.Deserialize<DataRunModel>(str, SRCApiClient.SerializerOptions);
        return model.Data;
    }

    public async Task<RunModel[]> GetAllRuns(RunFilterOptions options)
    {
        var query = options.GetFilterQueryString<RunFilterOptions>();
        var build = new UriBuilder(AllRunsEndpointUri)
        {
            Query = query
        };
        var str = await _http.GetStringAsync(build.Uri);
        var model = JsonSerializer.Deserialize<RunListModel>(str, SRCApiClient.SerializerOptions);
        return model.Data;
    }
}