using DAMLib.Abstractions.Data;
using Microsoft.Extensions.DependencyInjection;

namespace OriginalPreviewPlugin
{
    public static class ServicesConfiguration
    {
        public static void AddOriginalFilePreviews(this IServiceCollection services)
        {
            services.AddScoped<IPreviewImageFactory, OriginalPreviewImageFactory>();
        }
    }
}