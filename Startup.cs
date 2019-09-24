using System;

using MCRoll.Data;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace MCRoll
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            // ef core config
            ConfigDatabase();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            void ConfigDatabase()
            {
                var dbHost = Configuration.GetValue<String>("DB_HOST");
                var dbPort = Configuration.GetValue<String>("DB_PORT");
                var dbUser = Configuration.GetValue<String>("DB_USER");
                var dbPassword = Configuration.GetValue<String>("DB_PASSWORD");
                var dbName = Configuration.GetValue<String>("DB_NAME");
                String connectingString = $"Server={dbHost};Port={dbPort};Database={dbName};User={dbUser};Password={dbPassword};";
                services.AddDbContextPool<MCRollDbContext>(
                    options => options.UseMySql(connectingString,
                        mySqlOptions =>
                        {
                            mySqlOptions.UnicodeCharSet(CharSet.Utf8mb4);
                        }));
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                logger.LogInformation($"Inf:Environment:{env.EnvironmentName}");
                logger.LogDebug($"Debug:Environment:{env.EnvironmentName}");
                logger.LogWarning($"Warn:Environment:{env.EnvironmentName}");
                logger.LogError($"Error:Environment:{env.EnvironmentName}");
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseRouting();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}");
            });

            await DbInitializer.Initialize(app.ApplicationServices, env.IsDevelopment());
        }
    }
}