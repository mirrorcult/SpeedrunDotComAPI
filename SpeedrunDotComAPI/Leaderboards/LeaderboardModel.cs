using System.Text.Json.Serialization;
using JetBrains.Annotations;
using SpeedrunDotComAPI.Categories;
using SpeedrunDotComAPI.Games;
using SpeedrunDotComAPI.Levels;
using SpeedrunDotComAPI.Links;
using SpeedrunDotComAPI.Metadata;
using SpeedrunDotComAPI.Runs;
using SpeedrunDotComAPI.Users;
using SpeedrunDotComAPI.Utility;
using SpeedrunDotComAPI.Variables;

namespace SpeedrunDotComAPI.Leaderboards;

[PublicAPI]
public struct LeaderboardModel
{
    public string Weblink { get; set; }
    [JsonConverter(typeof(ObjectOrStringConverter<DataGameModel>))]
    public object Game { get; set; }
    [JsonConverter(typeof(ObjectOrStringConverter<DataCategoryModel>))]
    public object Category { get; set; }
    [JsonConverter(typeof(ObjectOrStringConverter<DataLevelModel>))]
    public object? Level { get; set; }
    /// <summary>
    ///     SRC seems to imply that embedding this will embed a list,
    ///     but it also says that the original value is just a string, so who knows.
    /// </summary>
    [JsonConverter(typeof(ObjectOrStringConverter<MetadataListModel>))]
    public object? Platform { get; set; }
    [JsonConverter(typeof(ObjectOrStringConverter<MetadataListModel>))]
    public object? Region { get; set; }
    public bool VideoOnly { get; set; }
    public RunTiming? Timing { get; set; }
    public Dictionary<string, string> Values { get; set; }
    public RunPlaceModel[] Runs { get; set; }
    public LinkModel[] Links { get; set; }
    
    // Embeddable.
    public UserListModel? Players { get; set; }
    // Embeddable.
    public VariableListModel? Variables { get; set; }
}

internal struct DataLeaderboardModel
{
    public LeaderboardModel Data { get; set; }
}

public struct LeaderboardListModel
{
    public LeaderboardModel[] Data { get; set; }
}