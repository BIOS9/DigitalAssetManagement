using System.Text.Json;
using System.Text.Json.Serialization;

namespace DAMLib.Abstractions.Metadata
{
    public interface IMetadataRecord
    {
        public void SerializeToJson(Utf8JsonWriter writer, JsonSerializerOptions options);
    }
}