using JetBrains.Annotations;
using SpeedrunComAPI.Games;
using SpeedrunComAPI.Links;
using SpeedrunComAPI.Users;

namespace SpeedrunComAPI.Series;

/// <remarks>
///     This is more or less a copy of <see cref="GameModel"/>.
/// </remarks>
[PublicAPI]
public struct SeriesModel
{
    public string Id { get; set; }
    public Dictionary<NameType, string> Names { get; set; }
    public string Abbreviation { get; set; }
    public string Weblink { get; set; }
    /// <summary>
    ///     Supposed to either be a Dictionary{string, ModeratorType} or a <see cref="UserListModel"/> if embedded, probably.
    ///     I couldn't really figure out a good way to make it a variant type, so no embedding for you, sorry.
    ///     string key is the user ID for the moderator.
    /// </summary>
    public Dictionary<string, ModeratorType> Moderators { get; set; }
    public string? Created { get; set; }
    public Dictionary<string, AssetModel?> Assets { get; set; }
    public LinkModel[] Links { get; set; }
}

public struct DataSeriesModel
{
    public SeriesModel Data { get; set; }
}

public struct SeriesListModel
{
    public SeriesModel[] Data { get; set; }
}