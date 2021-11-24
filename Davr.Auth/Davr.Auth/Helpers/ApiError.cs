using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;

namespace Davr.Auth.Helpers
{
    public class ApiError
    {
        public DateTime Timestamp { get; set; }

        public int Status { get; set; }

        public string Path { get; set; }

        public string Error { get; set; } // Repeat text status code

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IList<object> Errors { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        public ApiError(HttpStatusCode statusCode)
        {
            Timestamp = DateTime.UtcNow;
            Status = (int)statusCode;
        }
    }
}
