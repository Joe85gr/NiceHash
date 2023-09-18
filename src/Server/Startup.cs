using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Server.Builders;
using Server.Handlers;
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
            services.AddScoped<INiceHashHandler, NiceHashHandler>();
            services.AddScoped<INiceHashDataService, NiceHashDataService>();
            services.AddScoped<IDataService, DataService>();
            services.AddScoped<INiceHashDataBuilder, NiceHashDataBuilder>();
            
            services.AddMemoryCache();
            services.AddLogging();

            ConfigureHttpClients(services);
        }

        public void ConfigureHttpClients(IServiceCollection services)
        {
            var apiUrl = Configuration["NiceHashApi"];
            
            if (string.IsNullOrEmpty(apiUrl))
                throw new ArgumentException("NiceHash Api not configured in the config file!");
            
            services.AddHttpClient<INiceHashDataService, NiceHashDataService>(client => {
                client.BaseAddress = new Uri(apiUrl);
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