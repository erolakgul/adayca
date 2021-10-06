using adayca.ui.BaseInterface;
using adayca.ui.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace adayca.ui.Extentions
{
    public static class ServiceCollectionExtensions
    {
        [Obsolete]
        public static void ConfigureWritable<T>(
          this IServiceCollection services,
          IConfigurationSection section,
          string file = "appsettings.json") where T : class, new()
        {
            services.Configure<T>(section);
            services.AddTransient<ICustomOptions<T>>(provider =>
            {
                var configuration = (IConfigurationRoot)provider.GetService<IConfiguration>();
                var environment = provider.GetService<IHostingEnvironment>();
                var options = provider.GetService<IOptionsMonitor<T>>();
                return new CustomOptions<T>(environment, options, configuration, section.Key, file);
                // eğer 1.ci ağacın altındaki değer değiştirilecekse section.Key kullan "SERLOG"{"path":"aaa.log"} gibi ise STARTUP.CS
                // return new WritableOptions<T>(environment, options, configuration, section.Key, file);
            });
        }
    }
}
