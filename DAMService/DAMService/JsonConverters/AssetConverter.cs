using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using DAMLib.Abstractions.Models;

namespace DAMService.JsonConverters
{
    public class AssetConverter : JsonConverter<IAsset>
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(IAsset).IsAssignableFrom(objectType);
        }
        
        public override IAsset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, IAsset value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteNumber("id", value.Id);
            writer.WriteNumber("dateAdded", new DateTimeOffset(value.DateAdded).ToUnixTimeSeconds());
            writer.WriteEndObject();
        }
    }
}