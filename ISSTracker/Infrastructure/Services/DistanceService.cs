using GMap.NET;
using ISSTracker.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ISSTracker.Infrastructure.Services
{
    public class DistanceService : IDistanceService
    {
        public double CalculateDistanceBetweenPoints(PointLatLng point1, PointLatLng point2, double currentEarthRadiusInMeters = 6376500, double currentIssOrbitalHeight = 412000)
        {
            var d1 = point1.Lat * (Math.PI / 180.0);
            var num1 = point1.Lng * (Math.PI / 180.0);
            var d2 = point2.Lat * (Math.PI / 180.0);
            var num2 = point2.Lng * (Math.PI / 180.0) - num1;
            var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) + Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);
            return (currentEarthRadiusInMeters + currentIssOrbitalHeight) * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
        }
    }
}
