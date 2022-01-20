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
        public readonly string OriginalFileName;

        public DiskMetadata(string path, string originalFileName)
        {
            Path = path ?? throw new ArgumentNullException(nameof(path));
            OriginalFileName = originalFileName ?? throw new ArgumentNullException(nameof(originalFileName));
        }


        public void SerializeToJson(Utf8JsonWriter writer, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("path", Path);
            writer.WriteString("originalFileName", OriginalFileName);
            writer.WriteEndObject();
        }
    }
}