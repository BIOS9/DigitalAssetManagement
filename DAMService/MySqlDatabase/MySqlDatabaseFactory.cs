using System;
using DAMLib.Abstractions.Database;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using MySqlDatabase.Configuration;
using MySqlDatabase.Database;

namespace MySqlDatabase
{
    public class MySqlDatabaseFactory : IDatabaseFactory
    {
        private readonly ILogger _logger;
        public MySqlConnectionFactory ConnectionFactory { get; private set; }
        
        public MySqlDatabaseFactory(ILoggerFactory loggerFactory, IOptions<MySqlOptions> options)
        {
            // Initialize logging
            if (loggerFactory == null)
                throw new ArgumentNullException(nameof(loggerFactory));
            _logger = loggerFactory.CreateLogger(nameof(MySqlAssetDatabase));
        
            // Initialize options
            MySqlOptions config = options?.Value ?? throw new ArgumentNullException(nameof(options));
            
            // Initialize MySql
            ConnectionFactory = new MySqlConnectionFactory(config.ConnectionString);
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        }
        
        public IRepositoryDatabase GetRepositoryDatabase()
        {
            using MySqlConnection con = ConnectionFactory.CreateConnection();
            return new MySqlRepositoryDatabase(con);
        }

        public IAssetDatabase GetAssetDatabase(int repositoryId)
        {
            using MySqlConnection con = ConnectionFactory.CreateConnection();
            return new MySqlAssetDatabase(con, repositoryId);
        }
        
        public IAssetFileDatabase GetAssetFileDatabase(int repositoryId, int assetId) // Repository ID is not used since the mysql schema uses globally unique IDs
        {
            using MySqlConnection con = ConnectionFactory.CreateConnection();
            return new MySqlAssetFileDatabase(con, assetId);
        }
    }
}