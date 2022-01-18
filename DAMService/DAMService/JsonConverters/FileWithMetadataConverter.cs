using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using DAMLib.Abstractions.Metadata;
using DAMLib.Abstractions.Models;
using DAMService.Models;
using TagsMetadata;

namespace DAMService.JsonConverters
{
    public class FileWithMetadataConverter : JsonConverter<FileWithMetadata>
    {
        public override FileWithMetadata Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, FileWithMetadata value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteNumber("id", value.File.Id);
            writer.WriteNumber("dateAdded", new DateTimeOffset(value.File.DateAdded).ToUnixTimeSeconds());
            writer.WriteStartObject("metadata");
            foreach (KeyValuePair<string, IMetadataRecord> record in value.Metadata)
            {
                writer.WritePropertyName(record.Key);
                record.Value.SerializeToJson(writer, options);
            }
            writer.WriteEndObject();
            writer.WriteEndObject();
        }
    }
}