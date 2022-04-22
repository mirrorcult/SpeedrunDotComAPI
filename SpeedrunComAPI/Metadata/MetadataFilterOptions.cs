using SpeedrunComAPI.Query;

namespace SpeedrunComAPI.Metadata;

public struct MetadataFilterOptions : IFilterOptions
{
    public FilterDirection? Direction { get; set; }
    public string? OrderBy { get; set; }
    public int? Max { get; set; }
    public int? Offset { get; set; }
}