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
    public class MySqlRepositoryDatabase : IRepositoryDatabase
    {
        private readonly MySqlConnection _mySqlConnection;

        internal MySqlRepositoryDatabase(MySqlConnection mySqlConnection)
        {
            _mySqlConnection = mySqlConnection ?? throw new ArgumentNullException(nameof(mySqlConnection));
        }

        public async Task<IReadOnlyCollection<IAssetRepository>> GetAllAssetRepositoriesAsync()
        {
            IEnumerable<AssetRepositoryModel> repositories = await _mySqlConnection.QueryAsync<AssetRepositoryModel>("SELECT * FROM `asset_repositories`");
            return repositories.ToImmutableList();
        }

        public async Task<IAssetRepository> GetAssetRepositoryAsync(int id)
        {
            AssetRepositoryModel repository = await _mySqlConnection.QuerySingleOrDefaultAsync<AssetRepositoryModel>(
                "SELECT * FROM `asset_repositories` WHERE `id`=@id", 
                new { id });
            return repository;
        }
    }
}