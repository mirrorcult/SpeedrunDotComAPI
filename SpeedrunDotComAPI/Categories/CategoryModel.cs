using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using SpeedrunDotComAPI.Games;
using SpeedrunDotComAPI.Links;
using SpeedrunDotComAPI.Variables;

namespace SpeedrunDotComAPI.Categories;

[PublicAPI]
public struct CategoryModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Weblink { get; set; }
    public CategoryType Type { get; set; }
    public string Rules { get; set; }
    /// <remarks>
    ///     This is the "ruleset" for players allowed, not like the actual number of players in the category.
    ///     e.g. the portal 2 co-op any% category is "Exactly 2 players"
    /// </remarks>
    public CategoryPlayersModel Players { get; set; }
    public bool Miscellaneous { get; set; }
    public LinkModel[] Links { get; set; }
    
    // Embeddable.
    public DataGameModel? Game { get; set; }
    // Embeddable.
    public VariableListModel? Variables { get; set; }
}

[PublicAPI]
public struct CategoryPlayersModel
{
    public PlayersType Type { get; set; }
    public int Value { get; set; }
}

// This really shouldn't have to be done using this json extension and EnumMemberAttribute but
// system.text.json isn't working for me because of https://github.com/dotnet/runtime/issues/31619
// Thanks MS!
[PublicAPI, JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum CategoryType
{
    [EnumMember(Value = "per-game")]
    PerGame,
    [EnumMember(Value = "per-level")]
    PerLevel
}

[PublicAPI, JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum PlayersType
{
    Exactly,
    [EnumMember(Value = "up-to")]
    UpTo
}

public struct DataCategoryModel
{
    public CategoryModel Data { get; set; }
}

public struct CategoryListModel
{
    public CategoryModel[] Data { get; set; }
}