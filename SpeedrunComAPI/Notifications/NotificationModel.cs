using System.Text.Json.Serialization;
using JetBrains.Annotations;
using SpeedrunComAPI.Links;

namespace SpeedrunComAPI.Notifications;

[PublicAPI]
public struct NotificationModel
{
    public string Id { get; set; }
    public string Created { get; set; }
    public NotificationStatus Status { get; set; }
    public string Text { get; set; }
    public NotificationItemModel Item { get; set; }
    public LinkModel[] Links { get; set; }
}

public struct NotificationListModel
{
    public NotificationModel[] Data { get; set; }
}

[PublicAPI]
public struct NotificationItemModel
{
    public NotificationType Rel { get; set; }
    public Uri Uri { get; set; }
}

[PublicAPI, JsonConverter(typeof(JsonStringEnumConverter))]
public enum NotificationType
{
    Post,
    Run,
    Game,
    Guide
}

[PublicAPI, JsonConverter(typeof(JsonStringEnumConverter))]
public enum NotificationStatus
{
    Read,
    Unread
}