using JWTDemoClient.Configuration;
using JWTDemoClient.Services;
using JWTDemoClient.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JWTDemoClient
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => AppSettings = configuration.Get<AppSettings>();

        public AppSettings AppSettings { get; }

        public void ConfigureServices(IServiceCollection services) => services
            .AddSingleton(AppSettings)
            .AddScoped<OderService>()
            .AddScoped<AuthorizationService>()
            .AddDistributedMemoryCache()
            .AddCustomApiVersioning()
            .AddCustomSwaggerDocGenForApiVersioning()
            .AddCustomControllers()
            .AddCustomAuthentication()
            .AddHttpClient()
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
