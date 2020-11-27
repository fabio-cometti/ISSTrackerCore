using GMap.NET;

namespace ISSTracker.Infrastructure.Interfaces
{
    /// <summary>
    /// Services for calculate distance
    /// </summary>
    public interface IDistanceService
    {
        /// <summary>
        /// Calculate the distance in meters between two given points
        /// </summary>
        /// <param name="point1">The first point</param>
        /// <param name="point2">The second point</param>
        /// <param name="currentEarthRadiusInMeters">The radius of the earth used in the calc. If not supplied a default value of 6376500 meters is used</param>
        /// <param name="currentIssOrbitalHeight">The height of the ISS used in the calc. If not supplied a default value of 412000 is used</param>
        /// <returns>The distance between the two points in meters</returns>
        double CalculateDistanceBetweenPoints(PointLatLng point1, PointLatLng point2, double currentEarthRadiusInMeters = 6376500, double currentIssOrbitalHeight = 412000);
    }
}