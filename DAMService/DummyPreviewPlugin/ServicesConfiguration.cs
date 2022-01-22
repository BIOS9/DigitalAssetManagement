using DAMLib.Abstractions.Data;
using DAMLib.Abstractions.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DummyPreviewPlugin
{
    public static class ServicesConfiguration
    {
        public static void AddDummyFilePreviews(this IServiceCollection services)
        {
            services.AddScoped<IPreviewImageFactory, DummyPreviewImageFactory>();
        }
    }
}