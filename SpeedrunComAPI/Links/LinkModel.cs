using JetBrains.Annotations;

namespace SpeedrunComAPI.Links;

[PublicAPI]
public struct LinkModel
{
    /// <summary>
    ///     The relative path to this link.
    /// </summary>
    public string? Rel { get; set; }
    public Uri Uri { get; set; }
}

[PublicAPI]
public static class LinkModelExt
{
    public static Uri? GetLinkWithRel(this LinkModel[] links, string rel)
    {
        return links.Where(l => l.Rel == rel).Select(l => l.Uri).FirstOrDefault();
    }
}