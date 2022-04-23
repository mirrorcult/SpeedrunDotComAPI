using JetBrains.Annotations;
using SpeedrunDotComAPI.Query;

namespace SpeedrunDotComAPI.Levels;

[PublicAPI]
public struct LevelEmbedOptions : IEmbedOptions
{
    /// <summary>
    ///     For the per-level categories, not the parent category.
    /// </summary>
    public bool Categories { get; set; }
    public bool Variables { get; set; }
}

[PublicAPI]
public struct LevelRecordsFilterOptions : IFilterOptions
{
    public int? Top { get; set; }
    public bool? SkipEmpty { get; set; }
    public FilterDirection? Direction { get; set; }
    public string? OrderBy { get; set; }
    public int? Max { get; set; }
    public int? Offset { get; set; }
}

[PublicAPI]
public struct LevelVariablesFilterOptions : IFilterOptions
{
    public FilterDirection? Direction { get; set; }
    public string? OrderBy { get; set; }
    public int? Max { get; set; }
    public int? Offset { get; set; }
}