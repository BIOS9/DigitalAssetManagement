using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using DAMLib.Abstractions.Data;
using Dapper;
using Microsoft.Extensions.Logging;
using MimeTypes;
using MySql;
using MySql.Data.MySqlClient;

namespace DiskAssetFileSource
{
    public class DiskAssetFileSource : IAssetFileSource
    {
        private readonly MySqlConnectionFactory _mySqlConnectionFactory;
        private readonly ILoggerFactory _loggerFactory;

        public DiskAssetFileSource(MySqlConnectionFactory mySqlConnectionFactory, ILoggerFactory loggerFactory)
        {
            _mySqlConnectionFactory = mySqlConnectionFactory ?? throw new ArgumentNullException(nameof(mySqlConnectionFactory));
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

            string filePath = await GetFilePathAsync(con, fileId);
            if (basePath == null)
            {
                throw new DataException($"Asset file is missing a path. File ID: {fileId}");
            }

            string fullPath = Path.Join(basePath, filePath);
            string mimeType = MimeTypeMap.GetMimeType(Path.GetExtension(fullPath));

            return new DiskFileData(mimeType, fullPath, _loggerFactory);
        }
        
        public async Task<string> GetRepositoryBasePathAsync(MySqlConnection connection, int repositoryId)
        {
            return await connection.QuerySingleAsync<string>(
                "SELECT `base_path` FROM `repository_disk_info` WHERE `repository_id`=@repositoryId",
                new { repositoryId });
        }
        
        public async Task<string> GetFilePathAsync(MySqlConnection connection, int fileId)
        {
            return await connection.QuerySingleAsync<string>(
                "SELECT `path` FROM `asset_file_metadata_disk` WHERE `asset_file_id`=@fileId",
                new { fileId });
        }
    }
}