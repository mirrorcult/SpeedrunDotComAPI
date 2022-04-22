using System.Text.Json;
using System.Text.Json.Serialization;

namespace SpeedrunComAPI.Utility;

public class ArrayOrObjectConverter<TArray, TObject> : JsonConverter<object>
{
    public override object? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var thisToken = reader.TokenType;

        if (thisToken == JsonTokenType.StartArray)
        {
            return JsonSerializer.Deserialize<TArray>(ref reader, options);
        }
        if (thisToken == JsonTokenType.StartObject)
        {
            return JsonSerializer.Deserialize<TObject>(ref reader, options);
        }

        throw new JsonException("ArrayOrObjectConverter tried to deserialize non-array or string value");
    }

    public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, options);
    }
}