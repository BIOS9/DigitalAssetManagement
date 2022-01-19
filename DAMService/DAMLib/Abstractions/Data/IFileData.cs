using System.IO;
using System.Threading.Tasks;

namespace DAMLib.Abstractions.Data
{
    public interface IFileData
    {
        public Task<string> GetMimeTypeAsync();
        public Task<Stream> GetDataStreamAsync();
    }
}