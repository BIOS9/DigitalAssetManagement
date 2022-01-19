using System;
using System.IO;
using System.Threading.Tasks;
using DAMLib.Abstractions.Data;
using Microsoft.Extensions.Logging;

namespace DiskAssetFileSource
{
    public class DiskFileData : IFileData
    {
        private readonly string _mimeType;
        private readonly string _filePath;
        private readonly ILogger _logger;

        public DiskFileData(string mimeType, string filePath, ILoggerFactory loggerFactory)
        {
            _mimeType = mimeType ?? throw new ArgumentNullException(nameof(mimeType));
            _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            _logger = loggerFactory.CreateLogger(nameof(DiskFileData));
        }

        public Task<string> GetMimeTypeAsync()
        {
            return Task.FromResult(_mimeType);
        }

        public Task<Stream> GetDataStreamAsync()
        {
            try
            {
                return Task.FromResult((Stream)new FileStream(_filePath, FileMode.Open, FileAccess.Read));
            }
            catch (DirectoryNotFoundException ex)
            {
                _logger.LogWarning($"Failed to find directory for file \"{_filePath}\"", ex);
                return Task.FromResult<Stream>(null);
            }
            catch (FileNotFoundException ex)
            {
                _logger.LogWarning($"Failed to find file \"{_filePath}\"", ex);
                return Task.FromResult<Stream>(null);
            }
        }
    }
}