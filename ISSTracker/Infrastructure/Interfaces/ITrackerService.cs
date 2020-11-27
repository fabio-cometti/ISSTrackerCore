using ISSTracker.Infrastructure.Models;
using System.Threading.Tasks;

namespace ISSTracker.Infrastructure.Interfaces
{
    /// <summary>
    /// Service for tracking ISS position
    /// </summary>
    public interface ITrackerService
    {
        /// <summary>
        /// Retrieve the ISS current position from public web API. see http://api.open-notify.org/
        /// </summary>
        /// <returns>Current position of the ISS</returns>
        Task<IssApiResponse> GetISSCurrentPosition();
    }
}