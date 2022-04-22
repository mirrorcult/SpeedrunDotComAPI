using JetBrains.Annotations;
using SpeedrunComAPI.Query;

namespace SpeedrunComAPI.Leaderboards;

[PublicAPI]
public struct LeaderboardEmbedOptions : IEmbedOptions
{
    public bool Game { get; set; }
    public bool Category { get; set; }
    public bool Level { get; set; }
    public bool Players { get; set; }
    public bool Regions { get; set; }
    public bool Platforms { get; set; }
    public bool Variables { get; set; }
}

[PublicAPI]
public struct LeaderboardFilterOptions : IFilterOptions
{
    public int? Top { get; set; }
    public FilterDirection? Direction { get; set; }
    public string? OrderBy { get; set; }
    public int? Max { get; set; }
    public int? Offset { get; set; }
}