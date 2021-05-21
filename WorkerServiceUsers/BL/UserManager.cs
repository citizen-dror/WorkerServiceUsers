using Microsoft.Extensions.DependencyInjection;
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
        string getName();
    }

    // a singlotn to manage users
    public class UserManager : IUserManager
    {
        private List<CustomTimer> TimersList;
        public string Name;

        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<UserManager> _logger;

        public UserManager(
            IServiceScopeFactory scopeFactory,
            ILogger<UserManager> logger)
        {
            this._scopeFactory = scopeFactory;
            this._logger = logger;
            Name = "kdalaomer";
            _logger.LogInformation("init UserManager");
        }

        public int ManageUsers()
        {
            _logger.LogInformation("ManageUsers");
            int res;
            TimersList = new List<CustomTimer> { };
            using (var scope = _scopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<supercomDbContext>();
                var UserList = db.Users.Where(p => p.NextTask < DateTime.Now).ToList();
                foreach (User user in UserList)
                {
                    CustomTimer timer = StartTimer(user);
                    TimersList.Add(timer);
                }
                //db.Users.UpdateRange(UserList);
                //db.SaveChanges();
                res = UserList.Count;
            }

            return res;
        }
        public string getName()
        {
            return Name;
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
            _logger.LogInformation($"event for user {name} at {e.SignalTime}");
        }
    }

    class CustomTimer : Timer
    {
        public string name;
        public long id;
    }
}
