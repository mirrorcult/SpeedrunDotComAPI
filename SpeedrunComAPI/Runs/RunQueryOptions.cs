using JetBrains.Annotations;
using SpeedrunComAPI.Query;

namespace SpeedrunComAPI.Runs;

[PublicAPI]
public struct RunEmbedOptions : IEmbedOptions
{
    public bool Game { get; set; }
    public bool Category { get; set; }
    public bool Level { get; set; }
    public bool Players { get; set; }
    public bool Region { get; set; }
    public bool Platform { get; set; }
}

[PublicAPI]
public struct RunFilterOptions : IFilterOptions
{
    public string? User { get; set; }
    public string? Guest { get; set; }
    public string? Examiner { get; set; }
    public string? Game { get; set; }
    public string? Category { get; set; }
    public string? Level { get; set; }
    public string? Region { get; set; }
    public string? Platform { get; set; }
    public bool? Emulated { get; set; }
    public RunStatus? Status { get; set; }
    public FilterDirection? Direction { get; set; }
    public string? OrderBy { get; set; }
    public int? Max { get; set; }
    public int? Offset { get; set; }
}