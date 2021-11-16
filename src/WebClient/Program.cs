using System;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using WebClient.Services;

namespace WebClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            ConfigureServices(builder);
            ConfigureHttpClients(builder);

            await builder.Build().RunAsync();
        }
        
        private static void ConfigureServices(WebAssemblyHostBuilder builder)
        {
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped<IStorageService, StorageService>();
        }

        private static void ConfigureHttpClients(WebAssemblyHostBuilder builder)
        {
            builder.Services.AddHttpClient<IDataService, DataService>( client => {
                client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress); 
            });
        }
    }
}