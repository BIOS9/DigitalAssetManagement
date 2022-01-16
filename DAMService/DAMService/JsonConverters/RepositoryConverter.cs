using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using DAMLib.Abstractions.Models;

namespace DAMService.JsonConverters
{
    public class RepositoryConverter : JsonConverter<IAssetRepository>
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(IAssetRepository).IsAssignableFrom(objectType);
        }
        
        public override IAssetRepository Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, IAssetRepository value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteNumber("id", value.Id);
            writer.WriteString("name", value.Name);
            writer.WriteNumber("dateAdded", new DateTimeOffset(value.DateAdded).ToUnixTimeSeconds());
            writer.WriteEndObject();
        }
    }
}