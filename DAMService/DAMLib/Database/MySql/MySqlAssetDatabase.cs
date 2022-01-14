using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data.Common;
using System.Threading.Tasks;
using DAMLib.Abstractions;
using DAMLib.Database.MySql.Configuration;
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
        }

        public async Task<IReadOnlyCollection<IAssetRepository>> GetAllAssetRepositoriesAsync()
        {
            await using var con = _connectionFactory.CreateConnection();
            IEnumerable<MySqlAssetRepository> repositories = await con.QueryAsync<MySqlAssetRepository>("SELECT * FROM `asset_repositories`");
            return repositories.ToImmutableList();
        }

        public Task<IAssetRepository> GetAssetRepositoryAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}