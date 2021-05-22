using System;
using System.Collections.Generic;

#nullable disable

namespace WorkerServiceUsers.DAL
{
    public partial class User
    {
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string WebSite { get; set; }
        public string TimeZone { get; set; }
        public string PhoneSkype { get; set; }
        public string About { get; set; }
        public byte[] Avatar { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool? SendTasks { get; set; }
        public int TaskInterval { get; set; }
        public DateTime NextTask { get; set; }
    }
}
