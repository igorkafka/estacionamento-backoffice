using EstacionamentoBackoffice.API.Extensions;
using EstacionamentoBackoffice.Business.Interfaces;
using EstacionamentoBackoffice.Business.Notificacoes;
using EstacionamentoBackoffice.Data.Context;
using EstacionamentoBackoffice.Data.Repository;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EstacionamentoBackoffice.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<MeuDbContext>();
            services.AddScoped<IFormaPagamentoRepository, FormaPagamentoRepository>();
            services.AddScoped<ICarroRepository, CarroRepository>();
            services.AddScoped<IGaragemRepository, GaragemRepository>();

            services.AddScoped<INotificador, Notificador>();
            //services.AddScoped<IFornecedorService, FornecedorService>();
            //services.AddScoped<IProdutoService, ProdutoService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            return services;
        }
    }
}
