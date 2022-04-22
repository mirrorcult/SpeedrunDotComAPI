using JetBrains.Annotations;
using SpeedrunComAPI.Query;

namespace SpeedrunComAPI.Categories;

[PublicAPI]
public struct CategoryEmbedOptions : IEmbedOptions
{
    public bool Game { get; set; }
    public bool Variables { get; set; }
}

[PublicAPI]
public struct CategoryFilterOptions : IFilterOptions
{
    public int? Top { get; set; }
    public FilterDirection? Direction { get; set; }
    public string? OrderBy { get; set; }
    public int? Max { get; set; }
    public int? Offset { get; set; }
}

[PublicAPI]
public struct CategoryVariableFilterOptions : IFilterOptions
{
    public FilterDirection? Direction { get; set; }
    public string? OrderBy { get; set; }
    public int? Max { get; set; }
    public int? Offset { get; set; }
}