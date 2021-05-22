﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using WorkerServiceUsers.DAL;

namespace WorkerServiceUsers.BL
{

    public interface IUserManager
    {
        int ManageUsers();
    }

    // a singlotn to manage users
    public class UserManager : IUserManager
    {
        private List<CustomTimer> _timersList;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<UserManager> _logger;

        public UserManager(
            IServiceScopeFactory scopeFactory,
            ILogger<UserManager> logger)
        {
            this._scopeFactory = scopeFactory;
            this._logger = logger;
             _logger.LogInformation("init UserManager");
        }

        public int ManageUsers()
        {
            _logger.LogInformation("ManageUsers");
            int res;
            _timersList = new List<CustomTimer> { };
            using (var scope = _scopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<supercomDbContext>();
                var UserList = db.Users.Where(p => p.SendTasks == true).ToList();
                foreach (User user in UserList)
                {
                    CustomTimer timer = StartTimer(user);
                    _timersList.Add(timer);
                }
                //db.Users.UpdateRange(UserList);
                //db.SaveChanges();
                res = UserList.Count;
            }
            return res;
        }


        private CustomTimer StartTimer(User user)
        {
            string name = user.FirstName + " " + user.LastName;
            var timer = new CustomTimer
            {
                id = user.UserId,
                name = name,
                Interval = 1000 * user.TaskInterval,
            };
            _logger.LogInformation($"start timer for user {name}");
            timer.Elapsed += timer_Elapsed;
            timer.Start();
            return timer;
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            string name = ((CustomTimer)sender).name;
            _logger.LogInformation($"event for user: {name}, at {e.SignalTime}");
        }
    }

    class CustomTimer : Timer
    {
        public string name;
        public long id;
    }
}
