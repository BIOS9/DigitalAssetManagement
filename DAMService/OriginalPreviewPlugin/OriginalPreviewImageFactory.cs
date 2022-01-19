using System;
using System.Threading.Tasks;
using DAMLib.Abstractions.Data;

namespace OriginalPreviewPlugin
{
    public class OriginalPreviewImageFactory : IPreviewImageFactory
    {
        private readonly IAssetFileSource _fileSource;

        public OriginalPreviewImageFactory(IAssetFileSource fileSource)
        {
            _fileSource = fileSource ?? throw new ArgumentNullException(nameof(fileSource));
        }

        public async Task<IPreviewImage> GetPreviewImageAsync(int repositoryId, int assetId, int fileId, int? maxWidth, int? maxHeight)
        {
            IFileData fileData = await _fileSource.GetFileDataAsync(repositoryId, assetId, fileId);
            if (fileData == null)
            {
                return null;
            }
            return new OriginalPreviewImage(fileData, maxWidth, maxHeight);
        }
    }
}