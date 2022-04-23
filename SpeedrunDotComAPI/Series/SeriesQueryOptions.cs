using JetBrains.Annotations;
using SpeedrunDotComAPI.Query;

namespace SpeedrunDotComAPI.Series;

[PublicAPI]
public struct SeriesEmbedOptions : IEmbedOptions
{
}

[PublicAPI]
public struct SeriesFilterOptions : IFilterOptions
{
    public FilterDirection? Direction { get; set; }
    public string? OrderBy { get; set; }
    public int? Max { get; set; }
    public int? Offset { get; set; }
}
