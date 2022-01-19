using System.IO;
using System.Threading.Tasks;

namespace DAMLib.Abstractions.Data
{
    public interface IAssetFileSource
    {
        public Task<IFileData> GetFileDataAsync(int repositoryId, int assetId, int fileId);
    }
}