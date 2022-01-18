using DAMLib.Abstractions.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AssetDatabase.Helpers
{
    public static class ServicesConfiguration
    {
        public static void AddAssetDatabase(this IServiceCollection services)
        {
            services.AddScoped<IDatabaseFactory, AssetDatabaseFactory>();
        }
    }
}