using System.Threading.Tasks;

namespace DAMLib.Abstractions.Metadata
{
    public interface IMetadataSource
    {
        public string Name { get; }
        public Task<IMetadataRecord> GetAssetFileMetadataAsync(int repositoryId, int assetId, int fileId);
    }
}