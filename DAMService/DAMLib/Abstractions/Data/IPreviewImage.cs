using System.IO;
using System.Threading.Tasks;

namespace DAMLib.Abstractions.Data
{
    public interface IPreviewImage
    {
        public Task<Stream> GetPngImageStreamAsync();
    }
}