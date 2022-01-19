using DAMLib.Abstractions.Data;
using Microsoft.Extensions.DependencyInjection;

namespace DiskAssetFileSource
{
    public static class ServicesConfiguration
    {
        public static void AddDiskAssetFileSource(this IServiceCollection services)
        {
            services.AddSingleton<IAssetFileSource, DiskAssetFileSource>();
        }
    }
}