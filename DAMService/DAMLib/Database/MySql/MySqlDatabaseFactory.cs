using System;
using DAMLib.Abstractions.Database;
using DAMLib.Database.MySql.Configuration;
using DAMLib.Database.MySql.Database;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;

namespace DAMLib.Database.MySql
{
    public class MySqlDatabaseFactory : IDatabaseFactory
    {
        private readonly ILogger _logger;
        private readonly MySqlConnectionFactory _connectionFactory;
        
        public MySqlDatabaseFactory(ILoggerFactory loggerFactory, IOptions<MySqlOptions> options)
        {
            // Initialize logging
            if (loggerFactory == null)
                throw new ArgumentNullException(nameof(loggerFactory));
            _logger = loggerFactory.CreateLogger(nameof(MySqlAssetDatabase));
        
            // Initialize options
            MySqlOptions config = options?.Value ?? throw new ArgumentNullException(nameof(options));
            
            // Initialize MySql
            _connectionFactory = new MySqlConnectionFactory(config.ConnectionString);
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        }
        
        public IRepositoryDatabase GetRepositoryDatabase()
        {
            using MySqlConnection con = _connectionFactory.CreateConnection();
            return new MySqlRepositoryDatabase(con);
        }

        public IAssetDatabase GetAssetDatabase(int repositoryId)
        {
            using MySqlConnection con = _connectionFactory.CreateConnection();
            return new MySqlAssetDatabase(con, repositoryId);
        }
        
        public IAssetFileDatabase GetAssetFileDatabase(int repositoryId, int assetId) // Repository ID is not used since the mysql schema uses globally unique IDs
        {
            using MySqlConnection con = _connectionFactory.CreateConnection();
            return new MySqlAssetFileDatabase(con, assetId);
        }
    }
}