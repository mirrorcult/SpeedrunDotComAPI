using JetBrains.Annotations;
using SpeedrunDotComAPI.Links;

namespace SpeedrunDotComAPI.Metadata;

// "Metadata" is just my catch-all term for the small shitty things which I'm not even sure why SRC made into separate
// models of their own, i.e. developers, gametype, engines
// These all just have id/name/links.

[PublicAPI]
public struct MetadataModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public LinkModel[] Links { get; set; }
}

public struct DataMetadataModel
{
    public MetadataModel Data { get; set; }
}

public struct MetadataListModel
{
    public MetadataModel[] Data { get; set; }
}

// Yeah this is the only one that's different.
[PublicAPI]
public struct PlatformModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public LinkModel[] Links { get; set; }
    /// <summary>
    ///     The year this platform was released.
    /// </summary>
    public int Released { get; set; }
}

public struct DataPlatformModel
{
    public PlatformModel Data { get; set; }
}

public struct PlatformListModel
{
    public PlatformModel[] Data { get; set; }
}