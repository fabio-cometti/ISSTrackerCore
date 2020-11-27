using GMap.NET;
using ISSTracker.Infrastructure.Interfaces;
using ISSTracker.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ISSTracker.Infrastructure.Services
{
    /// <summary>
    /// Service for tracking ISS position
    /// </summary>
    internal class TrackerService : ITrackerService
    {
        private readonly HttpClient client;
        private readonly string apiURL;

        /// <summary>
        /// Create a new instance of TrackerService
        /// </summary>
        /// <param name="client">An HttpClient</param>
        /// <param name="apiURL">the URL of ISS API</param>
        public TrackerService(HttpClient client, string apiURL)
        {
            if (client is null)
            {
                throw new ArgumentNullException($"parameter {nameof(client)} cannot be null");
            }

            if (string.IsNullOrEmpty(apiURL))
            {
                throw new ArgumentNullException($"parameter {nameof(apiURL)} is not valid");
            }

            this.client = client;
            this.apiURL = apiURL;
        }

        /// <summary>
        /// Retrieve the ISS current position from public web API. see http://api.open-notify.org/
        /// </summary>
        /// <returns>Current position of the ISS</returns>
        public async Task<IssApiResponse> GetISSCurrentPosition()
        {
            try
            {
                var response = await client.GetAsync(apiURL);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"ISS API respond with a status error code of {response.StatusCode}");
                }

                string stream = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(stream))
                {
                    throw new Exception("Content response is Empty");
                }

                IssApiResponse apiResult = JsonSerializer.Deserialize<IssApiResponse>(stream);
                return apiResult;
            }
            catch (Exception ex)
            {
                throw new Exception("Error calling ISS API", ex);
            }
        }


    }
}
