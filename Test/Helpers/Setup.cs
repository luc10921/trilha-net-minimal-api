using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using MinimalApi.Dominio.Interfaces;
using MinimalApi.Dominio.Servicos;
using Microsoft.Extensions.DependencyInjection;
using Test.Mocks;
using MinimalApi;

namespace Test.Helpers
{
    public class Setup
    {
        public const string PORT = "5001";
        public static TestContext testContext = default!;
        public static WebApplicationFactory<Startup> http = default!;
        public static HttpClient client = default!;

        public static void ClassInit(TestContext testContext)
        {
            Setup.testContext = testContext;
            Setup.http = new WebApplicationFactory<Startup>();

            Setup.http = Setup.http.WithWebHostBuilder(builder => {
                builder.UseSetting("https_port", PORT).UseEnvironment("Testing");

                builder.ConfigureServices(services => {
                    services.AddScoped<IAdministradorServico, AdministradorServicoMock>();
                });
                
            });

            Setup.client = Setup.http.CreateClient();

        }

        public static void ClassCleanup(){
            Setup.http.Dispose();
        }
    }
}