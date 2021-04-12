using NUnit.Framework;
using System.IO;
using SaonGroupTest.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Moq;
using SaonGroupTest.Tests.ExtensionMethods;
using SaonGroupTest.Client.Services;
using System.Net.Http;

namespace SaonGroupTest.Tests
{
    [SetUpFixture]
    public class Testing
    {
        private static IConfigurationRoot _configuration;
        private static IServiceScopeFactory _scopeFactory;

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();

            var startup = new Startup(_configuration);

            var services = new ServiceCollection();

            services.AddOptions();
            services.AddSingleton<IConfiguration>(_configuration);
            services.AddTransient<ICovidRapiApiService, CovidRapiApiService>();
            services.AddHttpClient();

            startup.ConfigureServices(services);

            SetupMockDefaults(services);

            _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();

        }


        private static void SetupMockDefaults(IServiceCollection services)
        {
            var httpClientFactoryService = Mock.Of<IHttpClientFactory>();
            services.SwapTransient(sp => httpClientFactoryService);

            var configurationService = Mock.Of<IConfiguration>();
            services.SwapTransient(sp => configurationService);
        }


        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
            // Method intentionally left empty.
        }
    }
}