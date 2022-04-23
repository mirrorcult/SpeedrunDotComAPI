using System.Text.Json;
using System.Text.Json.Serialization;

namespace SpeedrunDotComAPI.Utility;

/// <summary>
///     Deserializes either a string or object value into an object.
/// </summary>
/// <remarks>
///     Yes, SRC seriously makes me do this. Embeds for runs override the normal
///     string field so it can be either of them.
///     https://stackoverflow.com/questions/33080843/can-i-parse-json-either-into-a-string-or-another-concrete-type-as-object
///     thank you sir for somehow having this exact same conundrum.
/// </remarks>
public class ObjectOrStringConverter<T> : JsonConverter<object?>
{
    public override object? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var thisToken = reader.TokenType;

        if (thisToken == JsonTokenType.String)
        {
            return JsonSerializer.Deserialize<string>(ref reader, options);
        }
        if (thisToken == JsonTokenType.StartObject)
        {
            return JsonSerializer.Deserialize<T>(ref reader, options);
        }

        throw new JsonException("ObjectOrStringConverter tried to deserialize non-object or string value");
    }

    public override void Write(Utf8JsonWriter writer, object? value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, options);
    }
}