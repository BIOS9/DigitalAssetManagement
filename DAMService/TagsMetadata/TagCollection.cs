using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text.Json;
using System.Text.Json.Serialization;
using DAMLib.Abstractions.Metadata;

namespace TagsMetadata
{
    public class TagCollection : IMetadataRecord
    {
        public readonly IReadOnlyCollection<Tag> Tags;

        public TagCollection(IEnumerable<Tag> tags)
        {
            Tags = (tags ?? throw new ArgumentNullException(nameof(tags))).ToImmutableArray();
        }


        public void SerializeToJson(Utf8JsonWriter writer, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            foreach (Tag tag in Tags)
            {
                writer.WriteStringValue(tag.Name);
            }
            writer.WriteEndArray();
        }
    }
}