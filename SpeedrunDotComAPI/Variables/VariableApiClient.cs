using System.Text.Json;
using JetBrains.Annotations;

namespace SpeedrunDotComAPI.Variables;

[PublicAPI]
public class VariableApiClient
{
    public static Uri VariableEndpointUri = new(SRCApiClient.EndpointUri, "variables/");
    
    private HttpClient _http;

    public VariableApiClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<VariableModel> GetSingleVariable(string id)
    {
        var build = new UriBuilder(new Uri(VariableEndpointUri, id));
        var str = await _http.GetStringAsync(build.Uri);
        var model = JsonSerializer.Deserialize<DataVariableModel>(str, SRCApiClient.SerializerOptions);
        return model.Data; 
    }
}