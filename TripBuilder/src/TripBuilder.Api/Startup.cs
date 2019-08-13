using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Examples;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using TripBuilder.Api.DataProviding;
using TripBuilder.Api.Services.ShortestRouteGenerator;

namespace TripBuilder.Api
{
    public class Startup
    {
        private const string ProjectName = "TripBuilder Api";
        private const string SwaggerUrl = "/swagger/v1/swagger.json";

        private static readonly IReadOnlyDictionary<string, string> DefaultConfiguration =
            new Dictionary<string, string>
            {
                ["PORT"] = "5000"
            };

        private static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseUrls($"http://*:{DefaultConfiguration["PORT"]}")
                .UseStartup<Startup>()
                .UseKestrel()
                .UseDefaultServiceProvider((context, options) => options.ValidateScopes = true)
                .Build();

            host.Run();
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDataProvider, DataProvider>();
            services.AddSingleton<IShortestRouteGenerator, ShortestRouteGenerator>();
            
            services
                .AddLogging(builder => { builder.SetMinimumLevel(LogLevel.Trace); })
                .AddSwaggerGen(SetupSwaggerGen)
                .AddMvc();
        }
        
        public void Configure(IApplicationBuilder appBuilder)
        {
            appBuilder
                .UseStaticFiles()
                .UseSwagger()
                .UseSwaggerUI(SetupSwaggerUi)
                .UseMvc();
        }
        
        private static void SetupSwaggerGen(SwaggerGenOptions options)
        {
            options.SwaggerDoc("v1", new Info { Title = ProjectName, Version = "v1" });
            options.OperationFilter<ExamplesOperationFilter>();
        }

        private static void SetupSwaggerUi(SwaggerUIOptions options)
        {
            options.DocumentTitle = ProjectName;

            options.SwaggerEndpoint(SwaggerUrl, ProjectName);

            options.DocExpansion(DocExpansion.Full);
        }
    }
}