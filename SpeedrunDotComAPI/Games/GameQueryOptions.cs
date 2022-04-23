using JetBrains.Annotations;
using SpeedrunDotComAPI.Query;

namespace SpeedrunDotComAPI.Games;

[PublicAPI]
public struct GameEmbedOptions : IEmbedOptions
{
    public bool Levels { get; set; }
    public bool Categories { get; set; }
    public bool Gametypes { get; set; }
    public bool Platforms { get; set; }
    public bool Regions { get; set; }
    public bool Genres { get; set; }
    public bool Engines { get; set; }
    public bool Developers { get; set; }
    public bool Publishers { get; set; }
    public bool Variables { get; set; }
}

[PublicAPI]
public struct GameFilterOptions : IFilterOptions
{
    public string? Name { get; set; }
    public string? Abbreviation { get; set; }
    public int? Released { get; set; }
    public string? Gametype { get; set; }
    public string? Platform { get; set; }
    public string? Region { get; set; }
    public string? Genre { get; set; }
    public string? Engine { get; set; }
    public string? Developer { get; set; }
    public string? Publisher { get; set; }
    public string? Moderator { get; set; }
    // ReSharper disable once InconsistentNaming
    public bool? _Bulk { get; set; } 
    public FilterDirection? Direction { get; set; }
    public string? OrderBy { get; set; }
    public int? Max { get; set; }
    public int? Offset { get; set; }
}

[PublicAPI]
public struct GameCategoryFilterOptions : IFilterOptions
{
    public bool? Miscellaneous { get; set; }
    public FilterDirection? Direction { get; set; }
    public string? OrderBy { get; set; }
    public int? Max { get; set; }
    public int? Offset { get; set; }
}

[PublicAPI]
public struct GameGenericFilterOptions : IFilterOptions
{
    public FilterDirection? Direction { get; set; }
    public string? OrderBy { get; set; }
    public int? Max { get; set; }
    public int? Offset { get; set; }
}

[PublicAPI]
public struct GameRecordsFilterOptions : IFilterOptions
{
    public int? Top { get; set; }
    public string? Scope { get; set; }
    public bool? Miscellaneous { get; set; }
    public bool? SkipEmpty { get; set; }
    public FilterDirection? Direction { get; set; }
    public string? OrderBy { get; set; }
    public int? Max { get; set; }
    public int? Offset { get; set; }
}