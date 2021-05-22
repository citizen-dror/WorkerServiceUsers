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
using Microsoft.Extensions.Configuration;

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
                    //get connection string from appsetting 
                    IConfiguration configuration = hostContext.Configuration;
                    string connectionsString = configuration.GetSection("ConnectionStrings").GetSection("supercomDb").Value;
                    //inject DbContext
                    var optionsBuilder = new DbContextOptionsBuilder<supercomDbContext>();
                    optionsBuilder.UseSqlServer(connectionsString);
                    services.AddScoped<supercomDbContext>(s => new supercomDbContext(optionsBuilder.Options));
                    //inject singlton UserManager
                    services.AddSingleton<IUserManager, UserManager>();
                    //inject the main Worker   
                    services.AddHostedService<Worker>();
                });

            return host;
        }
    }
}
