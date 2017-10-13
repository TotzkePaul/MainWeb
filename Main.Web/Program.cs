using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Main.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run(); 
        }


        public static IWebHost BuildWebHost(string[] args)
        {
            var bindingConfig = new ConfigurationBuilder()
                .AddCommandLine(args)
                .Build();
            var serverport = bindingConfig.GetValue<int?>("port") ?? 5000;
            var serverurls = bindingConfig.GetValue<string>("server.urls") ?? string.Format("http://*:{0}", serverport);

            var configDictionary = new Dictionary<string, string>
            {
                {"server.urls", serverurls},
                {"port", serverport.ToString()}
            };

            return new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((builderContext, config) =>
                {
                    IHostingEnvironment env = builderContext.HostingEnvironment;

                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                        .AddUserSecrets<Startup>(optional: false)
                        .AddCommandLine(args)
                        .AddInMemoryCollection(configDictionary);
                })
                .UseIISIntegration()
                .UseDefaultServiceProvider((context, options) =>
                {
                    options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
                })
                .UseStartup<Startup>()
                .UseUrls(serverurls)
                .Build();
        }
    }
}
