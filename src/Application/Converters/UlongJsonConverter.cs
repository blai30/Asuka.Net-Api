using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AsukaApi.Application.Converters
{
    public class UlongJsonConverter : JsonConverter<ulong>
    {
        public override ulong Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            ulong value = ulong.Parse(reader.GetString() ?? string.Empty);
            return value;
        }

        public override void Write(Utf8JsonWriter writer, ulong value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
