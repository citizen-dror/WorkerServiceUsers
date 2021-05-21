using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WorkerServiceUsers.DAL;

namespace WorkerServiceUsers
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        private readonly IServiceProvider _provider;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                using var scope = _serviceScopeFactory.CreateScope();

                var dbContext = scope.ServiceProvider.GetRequiredService<supercomDbContext>();
                long id = 1;
                var name = dbContext.Users.Find(id).FirstName;
                _logger.LogInformation("first name: {name}", name);

                await Task.Delay(2000, stoppingToken);
            }
        }
    }
}
