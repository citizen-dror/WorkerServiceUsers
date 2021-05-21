using System;
using System.Collections.Generic;

#nullable disable

namespace WorkerServiceUsers.DAL
{
    public partial class TimeZone
    {
        public string CountryCode { get; set; }
        public string LatLon { get; set; }
        public string Name { get; set; }
        public string PortionOfCountryCovered { get; set; }
        public string Status { get; set; }
        public string UtcOffset { get; set; }
        public string UtcDstOffset { get; set; }
        public string Notes { get; set; }
    }
}
