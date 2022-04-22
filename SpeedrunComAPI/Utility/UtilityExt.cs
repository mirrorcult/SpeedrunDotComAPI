namespace SpeedrunComAPI.Utility;

internal static class UtilityExt
{
    internal static string ComposeQueryString(string first, string second)
    {
        return first + (!string.IsNullOrEmpty(second) ? $"&{second}" : "");
    }
}