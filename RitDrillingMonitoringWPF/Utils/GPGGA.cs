using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RitDrillingMonitoringWPF.Utils
{
    internal class GPGGA
    {
        public TimeOnly UtcTime;
        public float latitude;
        public CardinalDirections latDirection;
        public float longtitude;
        public CardinalDirections lngDirection;
        public int solutionType;
        public int satellitesUsedCount;
        public float HDOP;
        public float altitudeAboveSeaLevel;
        public float altitudeGeoidAboveEllipsoid;
        public TimeOnly timeFromLastCorrectionDGPS;
        public string identifierDGPS;

        public GPGGA(
            TimeOnly utcTime,
            float latitude,
            CardinalDirections latDirection,
            float longtitude,
            CardinalDirections lngDirection,
            int solutionType,
            int satellitesUsedCount,
            float hDOP,
            float altitudeAboveSeaLevel,
            float altitudeGeoidAboveEllipsoid,
            TimeOnly timeFromLastCorrectionDGPS,
            string identifierDGPS
            )   
        {
            UtcTime = utcTime;
            this.latitude = latitude;
            this.longtitude = longtitude;
            this.solutionType = solutionType;
            this.satellitesUsedCount = satellitesUsedCount;
            HDOP = hDOP;
            this.altitudeAboveSeaLevel = altitudeAboveSeaLevel;
            this.altitudeGeoidAboveEllipsoid = altitudeGeoidAboveEllipsoid;
            this.timeFromLastCorrectionDGPS = timeFromLastCorrectionDGPS;
            this.identifierDGPS = identifierDGPS;
            this.latDirection = latDirection;
            this.lngDirection = lngDirection;
        }

        public string ToString()
        {
            string satellites = satellitesUsedCount > 9 ? satellitesUsedCount.ToString() : "0" + satellitesUsedCount.ToString();
            string message = $"$GPGGA,{GpsSerializer.CreateTime(UtcTime)},{latitude.ToString().Replace(",", ".")},{latDirection},{longtitude.ToString().Replace(",", ".")},{lngDirection},{solutionType},{satellites},{HDOP},{altitudeAboveSeaLevel},M,{altitudeGeoidAboveEllipsoid},M,{GpsSerializer.CreateTime(timeFromLastCorrectionDGPS)},{identifierDGPS}";
            return message;
        }

        
    }
}
