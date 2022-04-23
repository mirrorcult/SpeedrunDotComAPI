using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using SpeedrunDotComAPI.Links;

namespace SpeedrunDotComAPI.Variables;

[PublicAPI]
public struct VariableModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    /// <summary>
    ///     Null if this variable applies to all categories.
    /// </summary>
    public string? Category { get; set; }
    public VariableScopeModel Scope { get; set; }
    public bool Mandatory { get; set; }
    public bool UserDefined { get; set; }
    public bool Obsoletes { get; set; }
    public VariableOtherValuesModel Values { get; set; }
    public bool IsSubcategory { get; set; }
    public LinkModel[] Links { get; set; }
}

[PublicAPI]
public struct DataVariableModel
{
    public VariableModel Data { get; set; }
}

[PublicAPI]
public struct VariableListModel
{
    public VariableModel[] Data { get; set; }
}

/// <summary>
///     Defines the values on the variable model; i.e the 'other' values (e.g. 50cc, 100cc, 150cc) and the default value
/// </summary>
[PublicAPI]
public struct VariableOtherValuesModel
{
    /// <summary>
    ///     The key here is a variable ID.
    /// </summary>
    public Dictionary<string, VariableValueModel> Values { get; set; }
    /// <summary>
    ///     A variable ID for the default value.
    /// </summary>
    public string Default { get; set; }
}

/// <summary>
///     The actual data contained in the "other values"
/// </summary>
/// <remarks>
///     Sorry these names are confusing but in my defense the API isn't super great
/// </remarks>
[PublicAPI]
public struct VariableValueModel
{
    public string Label { get; set; }
    public string Rules { get; set; }
    public VariableValueFlagsModel Flags { get; set; }
}

// Come on man
[PublicAPI]
public struct VariableValueFlagsModel
{
    public bool? Miscellaneous { get; set; }
}

[PublicAPI]
public struct VariableScopeModel
{
    public VariableScopeType Type { get; set; }
    /// <summary>
    ///     Set only if <see cref="Type"/> is <see cref="VariableScopeType.SingleLevel"/>
    /// </summary>
    public string? Level { get; set; }
}

[PublicAPI, JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum VariableScopeType
{
    Global,
    [EnumMember(Value = "full-game")]
    FullGame,
    [EnumMember(Value = "all-levels")]
    AllLevels,
    [EnumMember(Value = "single-level")]
    SingleLevel
}
