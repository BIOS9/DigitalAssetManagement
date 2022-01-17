using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DAMLib.Abstractions.Data;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace DummyPreviewPlugin
{
    public class DummyPreviewImage : IPreviewImage
    {
        private const string _dummyImagePath = "files.png";

        private readonly int? _maxWidth;
        private readonly int? _maxHeight;

        public DummyPreviewImage(int? maxWidth, int? maxHeight)
        {
            this._maxWidth = maxWidth;
            this._maxHeight = maxHeight;
        }

        public async Task<Stream> GetPngImageStreamAsync()
        {
            MemoryStream ms = new MemoryStream();
            using (Stream previewImageStream = getPreviewImageStream())
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

        private Stream getPreviewImageStream()
        {
            var assembly = Assembly.GetExecutingAssembly();
            string resourceName = assembly.GetManifestResourceNames()
                .Single(str => str.EndsWith("files.png"));

            return assembly.GetManifestResourceStream(resourceName);
        }
    }
}