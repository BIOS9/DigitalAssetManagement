using DAMLib.Abstractions.Metadata;
using Microsoft.Extensions.DependencyInjection;

namespace GeneralMetadata
{
    public static class ServicesConfiguration
    {
        public static void AddMetadataGeneral(this IServiceCollection services)
        {
            services.AddScoped<IMetadataSource, GeneralMetadataProvider>();
        }
    }
}