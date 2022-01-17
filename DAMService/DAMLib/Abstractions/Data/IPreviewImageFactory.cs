using System.Threading.Tasks;

namespace DAMLib.Abstractions.Data
{
    public interface IPreviewImageFactory
    {
        public Task<IPreviewImage> GetPreviewImageAsync(int repositoryId, int assetId, int fileId, int? maxWidth, int? maxHeight);
    }
}