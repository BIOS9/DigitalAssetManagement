using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MySql.Configuration;

namespace MySql.Helpers
{
    public static class ServicesConfiguration
    {
        public static void AddMySql(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MySqlOptions>(configuration.GetSection(MySqlOptions.MySql));
            services.AddSingleton<IValidateOptions<MySqlOptions>, MySqlOptionsValidation>();
            services.AddSingleton<MySqlConnectionFactory>();
        }
    }
}