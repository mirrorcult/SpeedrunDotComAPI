using System.Text.Json.Serialization;
using JetBrains.Annotations;
using SpeedrunComAPI.Links;

namespace SpeedrunComAPI.Users;

[PublicAPI]
public struct UserModel
{
    public string Id { get; set; }
    public Dictionary<NameType, string?> Names { get; set; }
    public string? Pronouns { get; set; }
    public Uri Weblink { get; set; }
    public NameStyleModel NameStyle { get; set; }
    public UserRole Role { get; set; }
    public string? Signup { get; set; }
    public UserLocationModel? Location { get; set; }
    public LinkModel? Twitch { get; set; }
    public LinkModel? Hitbox { get; set; }
    public LinkModel? Youtube { get; set; }
    public LinkModel? Twitter { get; set; }
    public LinkModel? Speedrunslive { get; set; }
    public LinkModel[] Links { get; set; }
}

[PublicAPI]
public struct UserListModel
{
    public UserModel[] Data { get; set; }
}

internal struct DataUserModel
{
    public UserModel Data { get; set; }
}

[PublicAPI]
public struct UserLocationModel
{
    public AreaModel? Country;
    public AreaModel? Region;
}

[PublicAPI]
public struct AreaModel
{
    public string Code { get; set; }
    public Dictionary<NameType, string> Names { get; set; }
}

[PublicAPI]
public struct NameColorModel
{
    public string Light { get; set; }
    public string Dark { get; set; }
}

/// <remarks>
///     The API docs slightly lie here? <see cref="ColorFrom"/> and <see cref="ColorTo"/> are always valid.
///     It also appears that <see cref="NameStyleType.Solid"/> is never actually used and solid color names are just
///     a gradient where <see cref="ColorFrom"/> and <see cref="ColorTo"/> are identical. Okay, sure.
/// </remarks>
[PublicAPI]
public struct NameStyleModel
{
    public NameStyleType Style { get; set; }
    public NameColorModel ColorFrom { get; set; }
    public NameColorModel ColorTo { get; set; }
    public NameColorModel? Color { get; set; }
}

[PublicAPI, JsonConverter(typeof(JsonStringEnumConverter))]
public enum NameStyleType
{
    Solid,
    Gradient
}

[PublicAPI, JsonConverter(typeof(JsonStringEnumConverter))]
public enum NameType
{
    International,
    Japanese,
    Twitch
}

[PublicAPI, JsonConverter(typeof(JsonStringEnumConverter))]
public enum UserRole
{
    Guest,
    Banned,
    User,
    Trusted,
    Moderator,
    Admin,
    Programmer
}