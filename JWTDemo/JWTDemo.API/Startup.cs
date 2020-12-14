using JWTDemo.API.Configuration;
using JWTDemo.API.Services;
using JWTDemo.Domain.Services.Common;
using JWTDemo.Infra.Settings;
using JWTDemo.RepositoryDb.Repositories.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JWTDemo.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => AppSettings = configuration.Get<AppSettings>();

        public AppSettings AppSettings { get; }

        public void ConfigureServices(IServiceCollection services) => services
            .AddSingleton(AppSettings)
            .AddSingleton<TokenService>()
            .AddMemoryDb()
            .AddCustomApiVersioning()
            .AddCustomSwaggerDocGenForApiVersioning()
            .AddScopedByBaseType<ServiceBase>()
            .AddScopedByBaseType<RepositoryDbBase>()
            .AddCustomControllers()
            .AddCustomAuthentication(AppSettings)
            ;

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider) => app
            .UseCustomCors()
            .UseDeveloperExceptionDetailsPage(env)
            .UseHttpsRedirection()
            .UseCustomSwagger(provider)
            .UseRouting()
            .UseAuthentication()
            .UseAuthorization()
            .UseControllersEndpoints()
            ;
    }
}
