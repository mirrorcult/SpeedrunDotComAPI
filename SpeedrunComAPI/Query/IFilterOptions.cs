using System.Text;
using JetBrains.Annotations;

namespace SpeedrunComAPI.Query;

[PublicAPI]
public interface IFilterOptions
{
    public FilterDirection? Direction { get; set; }
    
    /// <summary>
    ///     You'll have to look at the API for the correct values of these, sorry.
    /// </summary>
    public string? OrderBy { get; set; }
    
    /// <summary>
    ///     The max elements to return.
    ///     Must be a number between 0 and 200 at least according to SRC.
    /// </summary>
    public int? Max { get; set; }
    
    /// <summary>
    ///     The offset for pagination purposes.
    ///     See https://github.com/speedruncomorg/api/blob/master/version1/pagination.md
    /// </summary>
    /// <remarks>
    ///     A max of 200 and offset of 50 would give you entries 51 to 250. As far as I can tell.
    /// </remarks>
    public int? Offset { get; set; }
}

[PublicAPI]
public enum FilterDirection
{
    Desc,
    Asc
}

public static class FilterOptionsExt
{
    /// <summary>
    ///     Using reflection, converts an <see cref="IFilterOptions"/> with its properties into a query string
    ///     that the SRC API takes.
    /// </summary>
    public static string GetFilterQueryString<T>(this IFilterOptions filterOptions)
    {
        var sb = new StringBuilder();
        var type = typeof(T);

        foreach (var prop in type.GetProperties())
        {
            if (prop.GetValue(filterOptions) is not { } value)
                continue;

            if (sb.Length != 0)
                sb.Append('&');

            sb.Append($"{prop.Name.ToLowerInvariant()}={value.ToString()!}");
        }

        return sb.ToString();
    }
}