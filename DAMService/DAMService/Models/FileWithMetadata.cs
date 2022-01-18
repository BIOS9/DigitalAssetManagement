using System.Collections.Generic;
using DAMLib.Abstractions.Metadata;
using DAMLib.Abstractions.Models;

namespace DAMService.Models
{
    public class FileWithMetadata
    {
        public IAssetFile File;
        public Dictionary<string, IMetadataRecord> Metadata = new Dictionary<string, IMetadataRecord>();
    }
}