using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkerServiceUsers.DAL;
using WorkerServiceUsers.BL;
using Microsoft.Extensions.Logging;

namespace WorkerServiceUsers
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

        }
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {
                    var optionsBuilder = new DbContextOptionsBuilder<supercomDbContext>();
                    optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=supercom;Trusted_Connection=True;");//,
                    services.AddScoped<supercomDbContext>(s => new supercomDbContext(optionsBuilder.Options));
                    //add singlton UserManager
                    services.AddSingleton<IUserManager, UserManager>();
                    //services.AddSingleton<IUserManager, UserManager>(serviceProvider =>
                    //{
                    //    var logger = serviceProvider.GetRequiredService<ILogger<UserManager>>();
                    //    return new UserManager(logger);
                    //});
                    services.AddHostedService<Worker>();
                });

            return host;
        }
        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureServices((hostContext, services) =>
        //        {
        //            var configuration = hostContext.Configuration;
        //            services.AddHostedService<Worker>();
        //            services.AddScoped<supercomDbContext>(s => new supercomDbContext(configuration));
        //        });
    }
}
