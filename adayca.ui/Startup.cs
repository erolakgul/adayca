using adayca.ui.BaseInterface;
using adayca.ui.Extentions;
using adayca.ui.Models;
using adayca.ui.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace adayca.ui
{
    public class Startup
    {
        // log için kullanýlacak parameter
        //ILoggerFactory _loggerFactory;
        string projectPath = "";

        [Obsolete]
        public Startup(IConfiguration configuration
              //  , Microsoft.AspNetCore.Hosting.IHostingEnvironment env
              //, ILoggerFactory loggerFactory
            )
        {

            #region refresh app settings file
            //        IConfigurationBuilder builder = new ConfigurationBuilder()
            //.SetBasePath(env.ContentRootPath)
            //.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            //.AddEnvironmentVariables();

            //        this.Configuration = builder.Build();
            #endregion
           // _loggerFactory = loggerFactory;
            Configuration = configuration;
            projectPath = configuration.GetValue<string>(WebHostDefaults.ContentRootKey);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        [Obsolete]
        public void ConfigureServices(IServiceCollection services)
        {
            // önce Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation paketi yüklenir, html debug ta deðiþiklik sayfaya yansýmýyordu
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            //appsettings log dosyasý adý deðiþtirme
            //services.Configure<CustomConfiguration>(Configuration.GetSection("Serilog:WriteTo:0:Args:configureLogger:WriteTo:0:Args"));
            services.ConfigureWritable<CustomConfiguration>(Configuration.GetSection("SERLOG"));

            services.AddControllersWithViews();

            #region caching
            services.AddSingleton<ICustomMemoryCache, CustomMemoryCache>();
            #endregion

            #region variables
            services.AddSingleton<ICustomVariables, CustomVariables>();
            #endregion

            #region httpClient
            services.AddSingleton<ICustomHttpClient, CustomHttpClient>(); 
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [Obsolete]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            #region logging
            loggerFactory.AddNLog();
            loggerFactory.ConfigureNLog("nlog.config");
            LogManager.Configuration.Variables["basedir"] = projectPath + "\\Logs";
            #endregion

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            #region getting remote ip 08012020 HttpOverrides
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                   ForwardedHeaders.XForwardedProto
            });
            #endregion

        }
    }
}
