using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using WebOrders_PW.PageObjects;
using WebOrders_PW.PageObjects.OrderPage;
using WebOrders_PW.TestData;

namespace WebOrders_PW;

public class Startup
{
    public void ConfigureHost(IHostBuilder hostBuilder)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
            .AddUserSecrets(typeof(Startup).GetTypeInfo().Assembly)
            .AddEnvironmentVariables()
            .Build();

        hostBuilder.ConfigureHostConfiguration(builder => builder.AddConfiguration(config));
        hostBuilder.ConfigureServices(services =>
        {
            services.AddSingleton<OrderFaker>();
            services.AddScoped<AddNewOrder>();
            services.AddScoped<OrderPage>();
            services.AddScoped<LoginPage>();
        });

    }
}