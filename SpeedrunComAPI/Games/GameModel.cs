using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using SpeedrunComAPI.Categories;
using SpeedrunComAPI.Levels;
using SpeedrunComAPI.Links;
using SpeedrunComAPI.Metadata;
using SpeedrunComAPI.Runs;
using SpeedrunComAPI.Users;
using SpeedrunComAPI.Utility;
using SpeedrunComAPI.Variables;

namespace SpeedrunComAPI.Games;

/// <remarks>
///     Many of these values are nullable as they don't appear in _bulk queries. Otherwise, they're there, like assets
///     or links.
/// </remarks>
[PublicAPI]
public struct GameModel
{
    public string Id { get; set; }
    public Dictionary<NameType, string?> Names { get; set; }
    public string Abbreviation { get; set; }
    public int Released { get; set; }
    public string? ReleaseDate { get; set; }
    public Uri Weblink { get; set; }
    public GameRulesetModel Ruleset { get; set; }
    public bool Romhack { get; set; }
    [JsonConverter(typeof(ArrayOrObjectConverter<String[], MetadataListModel>))]
    public object Gametypes { get; set; }
    [JsonConverter(typeof(ArrayOrObjectConverter<String[], PlatformListModel>))]
    public object Platforms { get; set; }
    [JsonConverter(typeof(ArrayOrObjectConverter<String[], MetadataListModel>))]
    public object Regions { get; set; }
    [JsonConverter(typeof(ArrayOrObjectConverter<String[], MetadataListModel>))]
    public object Genres { get; set; }
    [JsonConverter(typeof(ArrayOrObjectConverter<String[], MetadataListModel>))]
    public object Developers { get; set; }
    [JsonConverter(typeof(ArrayOrObjectConverter<String[], MetadataListModel>))]
    public object Publishers { get; set; }
    /// <summary>
    ///     Supposed to either be a Dictionary{string, ModeratorType} or a <see cref="UserListModel"/> if embedded, probably.
    ///     I couldn't really figure out a good way to make it a variant type, so no embedding for you, sorry.
    ///     string key is the user ID for the moderator.
    /// </summary>
    public Dictionary<string, ModeratorType> Moderators { get; set; }
    public string Created { get; set; }
    public Dictionary<string, AssetModel?>? Assets { get; set; }
    public LinkModel[]? Links { get; set; }
    
    // Embeddable.
    public LevelListModel? Levels { get; set; }
    // Embeddable.
    public CategoryListModel? Categories { get; set; }
    // Embeddable.
    public VariableListModel? Variables { get; set; }
}

[PublicAPI]
public struct GameListModel
{
    public GameModel[] Data { get; set; }
}

/// <remarks>
///     Just for the purposes of properly deserializing/embedding.
/// </remarks>
public struct DataGameModel
{
    public GameModel Data { get; set; }
}

[PublicAPI]
public struct GameRulesetModel
{
    public bool ShowMilliseconds { get; set; }
    public bool RequiresVerification { get; set; }
    public bool RequiresVideo { get; set; }
    public RunTiming[] RunTimes { get; set; }
    public RunTiming DefaultTime { get; set; }
    public bool EmulatorsAllowed { get; set; }
}

[PublicAPI]
public struct AssetModel
{
    public Uri Uri { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
}

[PublicAPI, JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum ModeratorType
{
    [EnumMember(Value = "moderator")]
    Moderator,
    [EnumMember(Value = "super-moderator")]
    SuperModerator
}