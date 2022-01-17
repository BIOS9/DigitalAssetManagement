using System;
using System.Threading.Tasks;
using DAMLib.Abstractions.Data;

namespace DummyPreviewPlugin
{
    public class DummyPreviewImageFactory : IPreviewImageFactory
    {
        public Task<IPreviewImage> GetPreviewImageAsync(int repositoryId, int assetId, int fileId, int? maxWidth, int? maxHeight)
        {
            return Task.FromResult((IPreviewImage)new DummyPreviewImage(maxWidth, maxHeight));
        }
    }
}