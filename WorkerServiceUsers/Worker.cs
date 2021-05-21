using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WorkerServiceUsers.DAL;
using WorkerServiceUsers.BL;

namespace WorkerServiceUsers
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        private readonly IServiceScopeFactory _serviceScopeFactory;

        private readonly IUserManager _userManager;

        public Worker(ILogger<Worker> logger, 
            IServiceScopeFactory serviceScopeFactory,
            IUserManager userManager
            )
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            _userManager = userManager;
            _logger.LogInformation("start worker");
            //init list ot timers
            _logger.LogInformation("count user managed: {count}", _userManager.ManageUsers());
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                using var scope = _serviceScopeFactory.CreateScope();
  
                await Task.Delay(10_000, stoppingToken);
            }
        }
    }
}
