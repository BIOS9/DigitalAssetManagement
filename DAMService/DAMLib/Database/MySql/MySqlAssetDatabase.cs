using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAMLib.Abstractions;
using DAMLib.Database.MySql.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DAMLib.Database.MySql
{
    public class MySqlAssetDatabase : IAssetDatabase
    {
        private readonly ILogger _logger;
        private readonly MySqlOptions _options;

        public MySqlAssetDatabase(ILoggerFactory loggerFactory, IOptions<MySqlOptions> options)
        {
            // Initialize logging
            if (loggerFactory == null)
                throw new ArgumentNullException(nameof(loggerFactory));
            _logger = loggerFactory.CreateLogger(nameof(MySqlAssetDatabase));

            // Initialize options
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));

            _logger.LogInformation(_options.ConnectionString);
        }

        public async Task<IReadOnlyCollection<IAssetRepository>> GetAllAssetRepositoriesAsync()
        {
            _logger.LogInformation("Hello!");
            return null;
        }

        public Task<IAssetRepository> GetAssetRepositoryAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}