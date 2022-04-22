using System.Text.Json;
using JetBrains.Annotations;
using SpeedrunComAPI.Query;

namespace SpeedrunComAPI.Metadata;

[PublicAPI]
public class MetadataApiClient
{
    public static Uri DeveloperEndpointUri = new(SRCApiClient.EndpointUri, "developers/");
    public static Uri EngineEndpointUri = new(SRCApiClient.EndpointUri, "engines/");
    public static Uri GametypeEndpointUri = new(SRCApiClient.EndpointUri, "gametypes/");
    public static Uri GenreEndpointUri = new(SRCApiClient.EndpointUri, "genre/");
    public static Uri PlatformEndpointUri = new(SRCApiClient.EndpointUri, "platforms/");
    public static Uri PublisherEndpointUri = new(SRCApiClient.EndpointUri, "publishers/");
    public static Uri RegionEndpointUri = new(SRCApiClient.EndpointUri, "regions/");
    
    private HttpClient _http;

    public MetadataApiClient(HttpClient http)
    {
        _http = http;
    }

    #region Internal Helpers
    
    private async Task<MetadataModel> GetSingleMetadata(string id, Uri endpoint)
    {
        var build = new UriBuilder(new Uri(endpoint, id));
        var str = await _http.GetStringAsync(build.Uri);
        var model = JsonSerializer.Deserialize<DataMetadataModel>(str, SRCApiClient.SerializerOptions);
        return model.Data; 
    }

    private async Task<MetadataModel[]> GetAllMetadata(Uri endpoint, MetadataFilterOptions options = default)
    {        
        var query = options.GetFilterQueryString<MetadataFilterOptions>();
        var uriString = endpoint.ToString();
        // remove last `/`
        uriString = uriString.Remove(uriString.Length - 1);
        var build = new UriBuilder(uriString)
        {
            Query = query
        };
        var str = await _http.GetStringAsync(build.Uri);
        var model = JsonSerializer.Deserialize<MetadataListModel>(str, SRCApiClient.SerializerOptions);
        return model.Data; 
    }
    
    #endregion
    
    #region Public API

    public async Task<MetadataModel> GetSingleDeveloper(string id)
    {
        return await GetSingleMetadata(id, DeveloperEndpointUri);
    }

    public async Task<MetadataModel[]> GetAllDevelopers(MetadataFilterOptions options = default)
    {
        return await GetAllMetadata(DeveloperEndpointUri, options);
    }
    
    public async Task<MetadataModel> GetSingleEngine(string id)
    {
        return await GetSingleMetadata(id, EngineEndpointUri);
    }

    public async Task<MetadataModel[]> GetAllEngines(MetadataFilterOptions options = default)
    {
        return await GetAllMetadata(EngineEndpointUri, options);
    }

    public async Task<MetadataModel> GetSingleGametype(string id)
    {
        return await GetSingleMetadata(id, GametypeEndpointUri);
    }

    public async Task<MetadataModel[]> GetAllGametypes(MetadataFilterOptions options = default)
    {
        return await GetAllMetadata(GametypeEndpointUri, options);
    }

    public async Task<MetadataModel> GetSingleGenre(string id)
    {
        return await GetSingleMetadata(id, GenreEndpointUri);
    }

    public async Task<MetadataModel[]> GetAllGenres(MetadataFilterOptions options = default)
    {
        return await GetAllMetadata(GenreEndpointUri, options);
    }

    // YES this is c+p eat my ass
    public async Task<PlatformModel> GetSinglePlatform(string id)
    {
        var build = new UriBuilder(new Uri(PlatformEndpointUri, id));
        var str = await _http.GetStringAsync(build.Uri);
        var model = JsonSerializer.Deserialize<DataPlatformModel>(str, SRCApiClient.SerializerOptions);
        return model.Data; 
    }

    public async Task<PlatformModel[]> GetAllPlatforms(MetadataFilterOptions options = default)
    {
        var query = options.GetFilterQueryString<MetadataFilterOptions>();
        var uriString = PlatformEndpointUri.ToString();
        // remove last `/`
        uriString = uriString.Remove(uriString.Length - 1);
        var build = new UriBuilder(uriString)
        {
            Query = query
        };
        var str = await _http.GetStringAsync(build.Uri);
        var model = JsonSerializer.Deserialize<PlatformListModel>(str, SRCApiClient.SerializerOptions);
        return model.Data; 
    }

    public async Task<MetadataModel> GetSinglePublisher(string id)
    {
        return await GetSingleMetadata(id, PublisherEndpointUri);
    }

    public async Task<MetadataModel[]> GetAllPublishers(MetadataFilterOptions options = default)
    {
        return await GetAllMetadata(PublisherEndpointUri, options);
    }

    public async Task<MetadataModel> GetSingleRegion(string id)
    {
        return await GetSingleMetadata(id, RegionEndpointUri);
    }

    public async Task<MetadataModel[]> GetAllRegions(MetadataFilterOptions options = default)
    {
        return await GetAllMetadata(RegionEndpointUri, options);
    }

    #endregion
}