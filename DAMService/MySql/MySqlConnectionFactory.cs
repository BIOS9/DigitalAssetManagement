using System;
using Microsoft.Extensions.Options;
using MySql.Configuration;
using MySql.Data.MySqlClient;

namespace MySql
{
    public class MySqlConnectionFactory
    {
        private readonly MySqlOptions _options;

        public MySqlConnectionFactory(IOptions<MySqlOptions> options)
        {
            // Initialize options
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        }

        public MySqlConnection CreateConnection()
        {
            return new MySqlConnection(_options.ConnectionString);
        }
    }
}