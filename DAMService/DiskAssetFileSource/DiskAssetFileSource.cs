using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using DAMLib.Abstractions.Data;
using Dapper;
using DiskMetadata;
using Microsoft.Extensions.Logging;
using MimeTypes;
using MySql;
using MySql.Data.MySqlClient;

namespace DiskAssetFileSource
{
    public class DiskAssetFileSource : IAssetFileSource
    {
        private readonly MySqlConnectionFactory _mySqlConnectionFactory;
        private readonly DiskMetadataProvider _diskMetadataProvider;
        private readonly ILoggerFactory _loggerFactory;

        public DiskAssetFileSource(MySqlConnectionFactory mySqlConnectionFactory, DiskMetadataProvider diskMetadataProvider, ILoggerFactory loggerFactory)
        {
            _mySqlConnectionFactory = mySqlConnectionFactory ?? throw new ArgumentNullException(nameof(mySqlConnectionFactory));
            _diskMetadataProvider = diskMetadataProvider ?? throw new ArgumentNullException(nameof(diskMetadataProvider));
            _loggerFactory = loggerFactory;
        }

        public async Task<IFileData> GetFileDataAsync(int repositoryId, int assetId, int fileId)
        {
            using MySqlConnection con = _mySqlConnectionFactory.CreateConnection();
            string basePath = await GetRepositoryBasePathAsync(con, repositoryId);
            if (basePath == null)
            {
                throw new DataException($"Repository is missing a base path. Repo ID: {repositoryId}");
            }

            DiskMetadata.DiskMetadata diskMetadata = (DiskMetadata.DiskMetadata)await _diskMetadataProvider.GetAssetFileMetadataAsync(repositoryId, assetId, fileId);
            if (diskMetadata == null)
            {
                throw new DataException($"Asset file is missing a path. File ID: {fileId}");
            }

            string fullPath = Path.Join(basePath, diskMetadata.Path);
            string mimeType = MimeTypeMap.GetMimeType(Path.GetExtension(fullPath));

            return new DiskFileData(mimeType, fullPath, _loggerFactory);
        }
        
        public async Task<string> GetRepositoryBasePathAsync(MySqlConnection connection, int repositoryId)
        {
            return await connection.QuerySingleOrDefaultAsync<string>(
                "SELECT `base_path` FROM `repository_disk_info` WHERE `repository_id`=@repositoryId",
                new { repositoryId });
        }
    }
}