using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using WorkerServiceUsers.DAL;

namespace WorkerServiceUsers.Services
{
    public class UserService
    {
        private readonly supercomDbContext _context;

        public UserService(supercomDbContext context)
        {
            _context = context;
        }

        public bool testDb()
        {
            Console.WriteLine("testdb");
            bool res = false;
            try
            {
                using (var db = _context)
                {
                    res = db.Database.CanConnect();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType());
                throw new Exception(ex.ToString());
            }
            return res;
        }

        public User getUser(long userId)
        {
            User res = null;
            try
            {
                using (var db = _context)
                {
                    // Read
                    Console.WriteLine("Querying for a user");
                    res = db.Users.Find(userId);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType());
                throw new Exception(ex.ToString());
            }
            return res;
        }

        public List<User> getAllUsers()
        {
            List<User> res = null;
            try
            {
                using (var db = _context)
                {
                    // Read
                    Console.WriteLine("Querying for all users");
                    res = db.Users.OrderBy(b => b.UserId).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType());
                throw new Exception(ex.ToString());
            }
            return res;
        }

        public long addUser(DAL.User user)
        {
            long res = -1;
            // set default values for the user
            user.CreateDate = DateTime.Now;
            user.LastUpdate = DateTime.Now;
            user.LastTask = DateTime.Now;
            user.TaskInterval = 10; //default value is 10 minutes
            try
            {
                using (var db = _context)
                {
                    // Create
                    Console.WriteLine("Inserting a new user");
                    db.Users.Add(user);
                    db.SaveChanges();
                    res = user.UserId;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType());
                throw new Exception(ex.ToString());
            }
            return res;
        }
    }
}
