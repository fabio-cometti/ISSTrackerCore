using ISSTracker.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ISSTracker.Infrastructure.Services
{
    public class SpeedService : ISpeedService
    {
        /// <summary>
        /// Calculate speed of the ISS
        /// </summary>
        /// <param name="distance">The distance traveled by the ISS in Km</param>
        /// <param name="timeDiff">The time taken in seconds</param>
        /// <returns>The speed of the ISS in Km/h</returns>
        public double CalculateSpeed(double distance, long timeDiff)
        {
            if (distance <= 0)
            {
                throw new ArgumentException($"Invalid value of {distance} for parameter {nameof(distance)}");
            }

            if (timeDiff <= 0)
            {
                throw new ArgumentException($"Invalid value of {timeDiff} for parameter {nameof(timeDiff)}");
            }

            return (distance / timeDiff) * 3600;
        }
    }
}
