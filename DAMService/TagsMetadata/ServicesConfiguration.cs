using DAMLib.Abstractions.Metadata;
using Microsoft.Extensions.DependencyInjection;

namespace TagsMetadata
{
    public static class ServicesConfiguration
    {
        public static void AddMetadataTags(this IServiceCollection services)
        {
            services.AddScoped<IMetadataSource, TagsMetadataProvider>();
            services.AddScoped<TagsMetadataProvider>();
        }
    }
}