using SpeedrunComAPI.Query;

namespace SpeedrunComAPI.Notifications;

public struct NotificationFilterOptions : IFilterOptions
{
    public FilterDirection? Direction { get; set; }
    public string? OrderBy { get; set; }
    public int? Max { get; set; }
    public int? Offset { get; set; }
}