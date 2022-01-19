using AssetDatabase.Helpers;
using DAMService.JsonConverters;
using DiskAssetFileSource;
using DiskMetadata;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySql.Helpers;
using OriginalPreviewPlugin;
using TagsMetadata;

namespace DAMService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMySql(Configuration);
            services.AddAssetDatabase();
            services.AddMetadataTags();
            services.AddMetadataDisk();
            services.AddOriginalFilePreviews();
            services.AddDiskAssetFileSource();
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    options.JsonSerializerOptions.Converters.Add(new RepositoryConverter());
                    options.JsonSerializerOptions.Converters.Add(new AssetConverter());
                    options.JsonSerializerOptions.Converters.Add(new AssetFileConverter());
                    options.JsonSerializerOptions.Converters.Add(new FileWithMetadataConverter());
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}