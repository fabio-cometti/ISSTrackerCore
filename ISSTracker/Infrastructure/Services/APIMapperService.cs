using GMap.NET;
using ISSTracker.Infrastructure.Interfaces;
using ISSTracker.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ISSTracker.Infrastructure.Services
{
    /// <summary>
    /// Convert the API response in a set of coordinates used by GMapControl
    /// </summary>
    internal class APIMapperService : IAPIMapperService
    {
        /// <summary>
        /// Map an API response to PointLatLng
        /// </summary>
        /// <param name="apiResponse">The API response</param>
        /// <returns></returns>
        public PointLatLng Map(IssApiResponse apiResponse)
        {
            if (apiResponse is null)
            {
                throw new ArgumentException($"{nameof(apiResponse)} cannot be null");
            }
            if (apiResponse.iss_position is null ||
                string.IsNullOrEmpty(apiResponse.iss_position.latitude) ||
                string.IsNullOrEmpty(apiResponse.iss_position.longitude))
            {
                throw new ArgumentException($"{nameof(apiResponse)} is not well formed");
            }

            double latitude;
            double longitude;
            
            latitude = Convert.ToDouble(apiResponse.iss_position.latitude, CultureInfo.InvariantCulture);
            longitude = Convert.ToDouble(apiResponse.iss_position.longitude, CultureInfo.InvariantCulture);            

            if (latitude > 90 || latitude < -90 || longitude > 180 || longitude < -180)
            {
                throw new ArgumentException($"Coordinates lat:{latitude} lng:{longitude} are out of bounds");
            }

            return new PointLatLng(latitude, longitude);
        }
    }
}
