using GMap.NET;
using ISSTracker.Infrastructure.Models;

namespace ISSTracker.Infrastructure.Interfaces
{
    /// <summary>
    /// Convert the API response in a set of coordinates used by GMapControl
    /// </summary>
    public interface IAPIMapperService
    {
        /// <summary>
        /// Map an API response to PointLatLng
        /// </summary>
        /// <param name="apiResponse">The API response</param>
        /// <returns></returns>
        PointLatLng Map(IssApiResponse apiResponse);
    }
}