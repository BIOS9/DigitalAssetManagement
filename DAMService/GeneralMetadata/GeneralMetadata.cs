using System;
using System.Text.Json;
using DAMLib.Abstractions.Metadata;

namespace GeneralMetadata
{
    public class GeneralMetadata : IMetadataRecord
    {
        public readonly string Name;
        public readonly string Description;

        public GeneralMetadata(string name, string description)
        {
            Name = name ?? throw new ArgumentNullException(nameof(Name));
            Description = description ?? throw new ArgumentNullException(nameof(Description));
        }


        public void SerializeToJson(Utf8JsonWriter writer, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("name", Name);
            writer.WriteString("description", Description);
            writer.WriteEndObject();
        }
    }
}