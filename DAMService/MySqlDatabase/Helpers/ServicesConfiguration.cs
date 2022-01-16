using DAMLib.Abstractions.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MySqlDatabase.Configuration;

namespace MySqlDatabase.Helpers
{
    public static class ServicesConfiguration
    {
        public static void AddMySqlAssetDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MySqlOptions>(configuration.GetSection(MySqlOptions.MySql));
            services.AddSingleton<IValidateOptions<MySqlOptions>, MySqlOptionsValidation>();
            services.AddScoped<IDatabaseFactory, MySqlDatabaseFactory>();
        }
    }
}