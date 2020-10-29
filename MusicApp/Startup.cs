using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MusicApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages(options =>
            {
                options.Conventions.AddPageRoute("/Albums", "");
            });

            //services.AddHttpClient<AlbumClient>(client =>
            //    client.BaseAddress = new Uri("http://localhost:44322"));

            //SZ - Add Tye
            //GetServiceUri is an extension method to extend the IConfiguration interface to get based 
            //on the name of the service the host and port number for the service. Starting tye, environmental 
            //variables with the name service:{name}:host and service:{name}:port are configured, and GetServiceUri 
            //can be used to easily connect these values to return a URL. For using this API, the NuGet package Microsoft.Tye.Extensions.Configuration need to be referenced.
            services.AddHttpClient<AlbumClient>(client =>
            {
                client.BaseAddress = Configuration.GetServiceUri("musicapi");
            });
            //SZ - End Tye
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();

            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
