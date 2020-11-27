using System;
using System.Collections.Generic;
using System.Text;

namespace ISSTracker.Infrastructure.Models
{
    [Serializable]
    public class IssApiResponse
    {
        public string message { get; set; }
        public long timestamp { get; set; }
        public IssPosition iss_position { get; set; }
    }

    [Serializable]
    public class IssPosition
    {
        public string latitude { get; set; }
        public string longitude { get; set; }
    }
}