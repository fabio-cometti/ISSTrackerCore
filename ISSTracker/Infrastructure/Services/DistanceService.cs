using GMap.NET;
using ISSTracker.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ISSTracker.Infrastructure.Services
{
    /// <summary>
    /// Services for calculate distance
    /// </summary>
    internal class DistanceService : IDistanceService
    {
        private const double EARTH_RADIUS = 6376500.0;
        private const double ISS_ORBITAL_HEIGHT = 412000.0;

        /// <summary>
        /// Calculate the distance in meters between two given points
        /// </summary>
        /// <param name="point1">The first point</param>
        /// <param name="point2">The second point</param>
        /// <param name="currentEarthRadiusInMeters">The radius of the earth used in the calc. If not supplied a default value of 6376500 meters is used</param>
        /// <param name="currentIssOrbitalHeight">The height of the ISS used in the calc. If not supplied a default value of 412000 is used</param>
        /// <returns>The distance between the two points in meters</returns>
        public double CalculateDistanceBetweenPoints(PointLatLng point1, PointLatLng point2, double currentEarthRadiusInMeters = EARTH_RADIUS, double currentIssOrbitalHeight = ISS_ORBITAL_HEIGHT)
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
