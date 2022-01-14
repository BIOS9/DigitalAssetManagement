using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using DAMLib.Abstractions;
using DAMLib.Database.MySql.Configuration;
using DAMLib.Database.MySql.Models;
using Dapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;

namespace DAMLib.Database.MySql
{
    public class MySqlAssetDatabase : IAssetDatabase
    {
        private readonly ILogger _logger;
        private readonly MySqlOptions _options;
        private readonly MySqlConnectionFactory _connectionFactory;
        
        public MySqlAssetDatabase(ILoggerFactory loggerFactory, IOptions<MySqlOptions> options)
        {
            // Initialize logging
            if (loggerFactory == null)
                throw new ArgumentNullException(nameof(loggerFactory));
            _logger = loggerFactory.CreateLogger(nameof(MySqlAssetDatabase));

            // Initialize options
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
            
            // Initialize MySql
            _connectionFactory = new MySqlConnectionFactory(_options.ConnectionString);
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public async Task<IReadOnlyCollection<IAssetRepository>> GetAllAssetRepositoriesAsync()
        {
            await using var con = _connectionFactory.CreateConnection();
            IEnumerable<AssetRepositoryModel> repositories = await con.QueryAsync<AssetRepositoryModel>("SELECT * FROM `asset_repositories`");
            return repositories.Select(model => new MySqlAssetRepository(model)).ToImmutableList();
        }

        public async Task<IAssetRepository> GetAssetRepositoryAsync(int id)
        {
            await using var con = _connectionFactory.CreateConnection();
            AssetRepositoryModel model = await con.QuerySingleOrDefaultAsync<AssetRepositoryModel>(
                "SELECT * FROM `asset_repositories` WHERE `id`=@id",
                new { id });
            if (model == null)
            {
                return null;
            }

            return new MySqlAssetRepository(model);
        }
    }
}