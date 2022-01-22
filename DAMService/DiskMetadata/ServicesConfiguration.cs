using DAMLib.Abstractions.Metadata;
using Microsoft.Extensions.DependencyInjection;

namespace DiskMetadata
{
    public static class ServicesConfiguration
    {
        public static void AddMetadataDisk(this IServiceCollection services)
        {
            services.AddScoped<IMetadataSource, DiskMetadataProvider>();
            services.AddScoped<DiskMetadataProvider>();
        }
    }
}