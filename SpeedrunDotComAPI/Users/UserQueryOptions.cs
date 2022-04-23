using JetBrains.Annotations;
using SpeedrunDotComAPI.Query;

namespace SpeedrunDotComAPI.Users;

[PublicAPI]
public struct UserFilterOptions : IFilterOptions
{
    public string? Lookup { get; set; }
    public string? Name { get; set; }
    public string? Twitch { get; set; }
    public string? Hitbox { get; set; }
    public string? Twitter { get; set; }
    public string? Speedrunslive { get; set; }
    public FilterDirection? Direction { get; set; }
    public string? OrderBy { get; set; }
    public int? Max { get; set; }
    public int? Offset { get; set; }
}