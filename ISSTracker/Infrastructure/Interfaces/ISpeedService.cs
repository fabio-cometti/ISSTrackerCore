namespace ISSTracker.Infrastructure.Interfaces
{
    /// <summary>
    /// Service for calculate speed 
    /// </summary>
    public interface ISpeedService
    {
        /// <summary>
        /// Calculate speed of the ISS
        /// </summary>
        /// <param name="distance">The distance traveled by the ISS in Km</param>
        /// <param name="timeDiff">The time taken in seconds</param>
        /// <returns>The speed of the ISS in Km/h</returns>
        double CalculateSpeed(double distance, long timeDiff);
    }
}