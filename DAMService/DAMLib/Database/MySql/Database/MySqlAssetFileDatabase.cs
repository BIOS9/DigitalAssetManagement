using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using DAMLib.Abstractions.Database;
using DAMLib.Abstractions.Models;
using DAMLib.Database.MySql.Configuration;
using DAMLib.Database.MySql.Models;
using Dapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;

namespace DAMLib.Database.MySql.Database
{
    public class MySqlAssetFileDatabase : IAssetFileDatabase
    {
        private readonly MySqlConnection _mySqlConnection;
        private readonly int _assetId;
        
        internal MySqlAssetFileDatabase(MySqlConnection mySqlConnection, int assetId)
        {
            _mySqlConnection = mySqlConnection ?? throw new ArgumentNullException(nameof(mySqlConnection));
            _assetId = assetId;
        }

        public async Task<IReadOnlyCollection<IAssetFile>> GetAllAssetFilesAsync()
        {
            IEnumerable<AssetFileModel> files = await _mySqlConnection.QueryAsync<AssetFileModel>(
                "SELECT * FROM `asset_files` WHERE `asset_id`=@assetId",
                new { assetId = _assetId });
            return files.ToImmutableList();
        }

        public async Task<IAssetFile> GetAssetFileAsync(int id)
        {
            AssetFileModel file = await _mySqlConnection.QuerySingleOrDefaultAsync<AssetFileModel>(
                "SELECT * FROM `asset_files` WHERE `asset_id`=@assetId AND `id`=@id",
                new { assetId = _assetId, id});
            return file;
        }
    }
}