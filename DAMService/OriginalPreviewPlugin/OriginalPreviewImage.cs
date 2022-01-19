using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Threading.Tasks;
using DAMLib.Abstractions.Data;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace OriginalPreviewPlugin
{
    public class OriginalPreviewImage : IPreviewImage
    {
        private readonly IFileData _fileData;
        private readonly int? _maxWidth;
        private readonly int? _maxHeight;

        public OriginalPreviewImage(IFileData fileData, int? maxWidth, int? maxHeight)
        {
            _fileData = fileData;
            _maxWidth = maxWidth;
            _maxHeight = maxHeight;
        }

        public async Task<Stream> GetPngImageStreamAsync()
        {
            MemoryStream ms = new MemoryStream();
            using (Stream previewImageStream = await _fileData.GetDataStreamAsync())
            using (Image image = await Image.LoadAsync(previewImageStream))
            {
                decimal width = image.Width;
                decimal height = image.Height;

                if (_maxWidth != null && _maxWidth.Value < width)
                {
                    int maxWidth = _maxWidth.Value;
                    decimal scale = decimal.Divide(maxWidth, width);
                    width *= scale;
                    height *= scale;
                }

                if (_maxHeight != null && _maxHeight.Value < height)
                {
                    int maxHeight = _maxHeight.Value;
                    decimal scale = decimal.Divide(maxHeight, height);
                    width *= scale;
                    height *= scale;
                }

                image.Mutate(x => x.Resize((int) width, (int) height));
                await image.SaveAsPngAsync(ms);
                ms.Seek(0, SeekOrigin.Begin);
            }

            return ms;
        }
    }
}