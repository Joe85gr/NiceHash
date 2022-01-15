using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MediatR;
using Server.Orchestrators;
using Server.Services;
using WebClient.Services;

namespace Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
        
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddHealthChecks();
            services.AddScoped<INiceHashService, NiceHashService>();
            services.AddScoped<IDataService, DataService>();
            services.AddScoped<INiceHashOrchestrator, NiceHashOrchestrator>();
            services.AddMediatR(typeof(Startup));
            services.AddMemoryCache();
            services.AddLogging();

            ConfigureHttpClients(services);
        }

        public void ConfigureHttpClients(IServiceCollection services)
        {
            services.AddHttpClient<INiceHashService, NiceHashService>(client => {
                client.BaseAddress = new Uri("https://api2.nicehash.com");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}