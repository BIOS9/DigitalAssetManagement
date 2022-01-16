using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using DAMLib.Abstractions.Database;
using DAMLib.Abstractions.Models;
using Dapper;
using MySql.Data.MySqlClient;
using MySqlDatabase.Models;

namespace MySqlDatabase.Database
{
    public class MySqlAssetDatabase : IAssetDatabase
    {
        private readonly MySqlConnection _mySqlConnection;
        private readonly int _repositoryId;
        
        internal MySqlAssetDatabase(MySqlConnection mySqlConnection, int repositoryId)
        {
            _mySqlConnection = mySqlConnection ?? throw new ArgumentNullException(nameof(mySqlConnection));
            _repositoryId = repositoryId;
        }

        public async Task<IReadOnlyCollection<IAsset>> GetAllAssetsAsync()
        {
            IEnumerable<AssetModel> repositories = await _mySqlConnection.QueryAsync<AssetModel>(
                "SELECT * FROM `assets` WHERE `asset_repository_id`=@repositoryId",
                new { repositoryId = _repositoryId });
            return repositories.ToImmutableList();
        }

        public async Task<IAsset> GetAssetAsync(int id)
        {
            AssetModel repository = await _mySqlConnection.QuerySingleOrDefaultAsync<AssetModel>(
                "SELECT * FROM `assets` WHERE `asset_repository_id`=@repositoryId AND `id`=@id",
                new { repositoryId = _repositoryId, id});
            return repository;
        }
    }
}