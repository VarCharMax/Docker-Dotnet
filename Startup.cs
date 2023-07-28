using ExampleApp.Models;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;

namespace ExampleApp
{
    public class Startup {

        private readonly IConfigurationRoot Configuration;

        public Startup(IWebHostEnvironment env) {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddEnvironmentVariables()
                .Build();
        }

        public void ConfigureServices(IServiceCollection services) {

            var host = Configuration["DBHOST"] ?? "localhost";
            var port = Configuration["DBPORT"] ?? "3306";
            var password = Configuration["DBPASSWORD"] ?? "mysecret";

            var connectionString = $"server={host};port={port};database=products;user=root;password={password};";

            services.AddDbContext<ProductDbContext>(options =>
                options.UseMySQL(connectionString));

            services.AddSingleton<IConfiguration>(Configuration);
            services.AddTransient<IRepository, ProductRepository>();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (!env.IsDevelopment())
            {
                app.UseHsts();
                app.UseExceptionHandler("/Home/Error");
            }
            else
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseRouting();
            // Must be declared before Authentication.
            app.UseForwardedHeaders(new ForwardedHeadersOptions {
                //Forward client IP and originating scheme (HTTP/HTTPS). This is to support HTTPS redirects.
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
