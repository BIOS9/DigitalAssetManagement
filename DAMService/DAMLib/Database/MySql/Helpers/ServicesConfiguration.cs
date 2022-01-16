using DAMLib.Abstractions;
using DAMLib.Abstractions.Database;
using DAMLib.Database.MySql.Configuration;
using DAMLib.Database.MySql.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DAMLib.Database.MySql.Helpers
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