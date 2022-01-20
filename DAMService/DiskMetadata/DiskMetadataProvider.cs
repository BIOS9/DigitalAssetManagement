using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DAMLib.Abstractions.Metadata;
using Dapper;
using DiskMetadata.Models;
using MySql;
using MySql.Data.MySqlClient;

namespace DiskMetadata
{
    public class DiskMetadataProvider : IMetadataSource
    {
        private const string _name = "disk";
        public string Name => _name;
        
        private readonly MySqlConnectionFactory _mySqlConnectionFactory;

        public DiskMetadataProvider(MySqlConnectionFactory mySqlConnectionFactory)
        {
            _mySqlConnectionFactory = mySqlConnectionFactory;
        }

        public async Task<IMetadataRecord> GetAssetFileMetadataAsync(int repositoryId, int assetId, int fileId)
        {
            using MySqlConnection con = _mySqlConnectionFactory.CreateConnection();
            DiskMetadataModel diskMetadataModel = await con.QuerySingleOrDefaultAsync<DiskMetadataModel>(
                "SELECT * FROM `asset_file_metadata_disk` WHERE `asset_file_id`=@fileId",
                new { fileId });
            
            if (diskMetadataModel == null)
            {
                return null;
            }

            return new DiskMetadata(diskMetadataModel.Path, diskMetadataModel.OriginalFileName);
        }
    }
}