using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using DAMLib.Abstractions.Models;

namespace DAMService.JsonConverters
{
    public class AssetFileConverter : JsonConverter<IAssetFile>
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(IAssetFile).IsAssignableFrom(objectType);
        }
        
        public override IAssetFile Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, IAssetFile value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteNumber("id", value.Id);
            writer.WriteNumber("dateAdded", new DateTimeOffset(value.DateAdded).ToUnixTimeSeconds());
            writer.WriteEndObject();
        }
    }
}