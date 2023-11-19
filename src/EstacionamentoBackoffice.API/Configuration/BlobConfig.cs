using Azure.Storage.Blobs;
using EstacionamentoBackoffice.API.Interfaces;
using EstacionamentoBackoffice.API.Services;

namespace EstacionamentoBackoffice.API.Configuration
{
    public static class BlogConfig
    {
        public static IServiceCollection ConfigBlobClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(provider =>
             new BlobContainerClient(
               configuration.GetValue<string>("Azurite:ConnectionString"),
               configuration.GetValue<string>("Azurite:Container")));

            services.AddSingleton<IBlobService, BlobService>();
            return services;
        }
    }
}
