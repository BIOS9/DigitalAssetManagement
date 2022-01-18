using System;
using AssetDatabase.Database;
using DAMLib.Abstractions.Database;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MySql;
using MySql.Data.MySqlClient;

namespace AssetDatabase
{
    public class AssetDatabaseFactory : IDatabaseFactory
    {
        private readonly ILogger _logger;
        private readonly MySqlConnectionFactory _connectionFactory;
        
        public AssetDatabaseFactory(ILoggerFactory loggerFactory, MySqlConnectionFactory mySqlConnectionFactory)
        {
            // Initialize logging
            if (loggerFactory == null)
                throw new ArgumentNullException(nameof(loggerFactory));
            _logger = loggerFactory.CreateLogger(nameof(Database.AssetDatabase));

            _connectionFactory = mySqlConnectionFactory ?? throw new ArgumentNullException(nameof(mySqlConnectionFactory));
            
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        }
        
        public IRepositoryDatabase GetRepositoryDatabase()
        {
            using MySqlConnection con = _connectionFactory.CreateConnection();
            return new RepositoryDatabase(con);
        }

        public IAssetDatabase GetAssetDatabase(int repositoryId)
        {
            using MySqlConnection con = _connectionFactory.CreateConnection();
            return new Database.AssetDatabase(con, repositoryId);
        }
        
        public IAssetFileDatabase GetAssetFileDatabase(int repositoryId, int assetId) // Repository ID is not used since the mysql schema uses globally unique IDs
        {
            using MySqlConnection con = _connectionFactory.CreateConnection();
            return new AssetFileDatabase(con, assetId);
        }
    }
}