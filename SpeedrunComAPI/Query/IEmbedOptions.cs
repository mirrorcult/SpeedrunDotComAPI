using System.Text;

namespace SpeedrunComAPI.Query;

/// <summary>
///     Defines a type as being embed options, i.e. it has bool properties that can be converted into a query string.
/// </summary>
public interface IEmbedOptions
{
}

public static class EmbedOptionsExt
{
    /// <summary>
    ///     Using reflection, converts an <see cref="IEmbedOptions"/> with its properties into a query string
    ///     that the SRC API takes.
    /// </summary>
    public static string GetEmbedQueryString<T>(this IEmbedOptions embedOptions)
    {
        var sb = new StringBuilder();
        var type = typeof(T);

        foreach (var prop in type.GetProperties())
        {
            // Only works on bool properties.
            if (prop.GetValue(embedOptions) is not true)
                continue;

            sb.Append(sb.Length switch
            {
                0 => "embed=",
                _ => ","
            });

            sb.Append(prop.Name.ToLowerInvariant());
        }

        return sb.ToString();
    }
}