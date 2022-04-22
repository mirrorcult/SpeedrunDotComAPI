using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using SpeedrunComAPI.Categories;
using SpeedrunComAPI.Games;
using SpeedrunComAPI.Levels;
using SpeedrunComAPI.Links;
using SpeedrunComAPI.Metadata;
using SpeedrunComAPI.Users;
using SpeedrunComAPI.Utility;

namespace SpeedrunComAPI.Runs;

[PublicAPI]
public struct RunModel
{
    public string Id { get; set; }
    public string Weblink { get; set; }
    /// <summary>
    ///     Object if embedded, string ID otherwise.
    /// </summary>
    [JsonConverter(typeof(ObjectOrStringConverter<GameModel>))]
    public object Game { get; set; }
    /// <summary>
    ///     Object if embedded, string ID otherwise.
    /// </summary>
    [JsonConverter(typeof(ObjectOrStringConverter<LevelModel>))]
    public object? Level { get; set; }
    /// <summary>
    ///     Object if embedded, string ID otherwise.
    /// </summary>
    [JsonConverter(typeof(ObjectOrStringConverter<CategoryModel>))]
    public object? Category { get; set; }
    public RunVideoModel? Videos { get; set; }
    public string? Comment { get; set; }
    public RunStatusModel Status { get; set; }
    /// <summary>
    ///     Object if embedded, array otherwise.
    /// </summary>
    [JsonConverter(typeof(ArrayOrObjectConverter<RunPlayerModel[], UserListModel>))]
    public object Players { get; set; }
    public string? Date { get; set; }
    public string? Submitted { get; set; }
    public RunTimesModel Times { get; set; }
    public RunSystemModel System { get; set; }
    public LinkModel? Splits { get; set; }
    public Dictionary<string, string> Values { get; set; }
    public LinkModel[] Links { get; set; }
    
    // Embeddable.
    public MetadataModel? Region { get; set; }
    // Embeddable.
    public MetadataModel? Platform { get; set; }
}

[PublicAPI]
public struct RunVideoModel
{
    public string? Text { get; set; }
    public LinkModel[] Links { get; set; }
}

[PublicAPI]
public struct RunStatusModel
{
    public RunStatus Status { get; set; }
    public string Examiner { get; set; }
    public string? Reason { get; set; }
}

[PublicAPI]
public struct RunSystemModel
{
    public string? Platform { get; set; }
    public bool Emulated { get; set; }
    public string? Region { get; set; }
}

[PublicAPI]
public struct RunPlayerModel
{
    public UserRole Rel { get; set; }
    public string Id { get; set; }
    public Uri Uri { get; set; }
}

// Fuck consistency btw.
[PublicAPI]
public struct RunTimesModel
{
    public string Primary { get; set; }
    [JsonPropertyName("primary_t")]
    public float PrimaryTime { get; set; }
    
    public string Realtime { get; set; }
    [JsonPropertyName("realtime_t")]
    public float RealTimeTime { get; set; }
    
    [JsonPropertyName("realtime_noloads")]
    public string RealtimeNoloads { get; set; }
    [JsonPropertyName("realtime_noloads_t")]
    public float RealtimeNoloadsTime { get; set; }
    
    public string Ingame { get; set; }
    [JsonPropertyName("ingame_t")]
    public float IngameTime { get; set; }
}

[PublicAPI]
public struct RunListModel
{
    public RunModel[] Data { get; set; }
}

public struct RunPlaceModel
{
    public RunModel Run { get; set; }
    public int Place { get; set; }
    public DataGameModel? Game { get; set; }
}

public struct RunPlaceListModel
{
    public RunPlaceModel[] Data { get; set; }
}

internal struct DataRunModel
{
    public RunModel Data { get; set; }
}

[PublicAPI, JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum RunTiming
{
    [EnumMember(Value = "realtime")]
    Realtime,
    [EnumMember(Value = "realtime_noloads")]
    RealtimeNoloads,
    [EnumMember(Value = "ingame")]
    Ingame,
}

[PublicAPI, JsonConverter(typeof(JsonStringEnumConverter))]
public enum RunStatus
{
    New,
    Verified,
    Rejected
}