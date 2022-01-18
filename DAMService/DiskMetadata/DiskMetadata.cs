using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text.Json;
using DAMLib.Abstractions.Metadata;

namespace DiskMetadata
{
    public class DiskMetadata : IMetadataRecord
    {
        public readonly string Path;

        public DiskMetadata(string path)
        {
            Path = path ?? throw new ArgumentNullException(nameof(path));
        }


        public void SerializeToJson(Utf8JsonWriter writer, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("path", Path);
            writer.WriteEndObject();
        }
    }
}