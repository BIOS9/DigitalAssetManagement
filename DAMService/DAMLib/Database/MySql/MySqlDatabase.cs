using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAMLib.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DAMLib.Database.MySql
{
    public class MySqlDatabase : IDatabase
    {
        private readonly ILogger _logger;
        private readonly MySqlOptions _options;

        public MySqlDatabase(ILoggerFactory loggerFactory, IOptions<MySqlOptions> options)
        {
            // Initialize logging
            if(loggerFactory == null)
                throw new ArgumentNullException(nameof(loggerFactory));
            _logger = loggerFactory.CreateLogger(nameof(MySqlDatabase));
            
            // Initialize options
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        }

        public Task<IReadOnlyCollection<IAssetRepository>> GetAllAssetRepositoriesAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<IAssetRepository> GetAssetRepositoryAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}