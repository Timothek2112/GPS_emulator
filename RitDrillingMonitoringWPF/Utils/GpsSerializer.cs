using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RitDrillingMonitoringWPF.Utils
{
    internal class GpsSerializer
    {
        public GpsSerializer() { }

        public static string CreateTime(TimeOnly time)
        {
            string hours = time.Hour > 9 ? time.Hour.ToString() : "0" + time.Hour.ToString();
            string minutes = time.Minute > 9 ? time.Minute.ToString() : "0" + time.Minute.ToString();
            string seconds = time.Second > 9 ? time.Second.ToString() : "0" + time.Second.ToString();
            if (hours == "00" && minutes == "00" && seconds == "00")
                return "";
            return hours + minutes + seconds;
        }

        public static GPGGA CreateGPGGA(Marker marker)
        {
            return new GPGGA(new TimeOnly(DateTime.UtcNow.Hour, DateTime.UtcNow.Minute, DateTime.UtcNow.Second), (float)marker.Position.Lat, CardinalDirections.N, (float)marker.Position.Lng, CardinalDirections.W, 1, 9, 0.9f, 11, 23, new TimeOnly(), "*47");
        }
    }
}
