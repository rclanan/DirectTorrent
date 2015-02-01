using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DirectTorrent.Data.Yify.Models;

namespace DirectTorrent.Data.Yify.ApiWrapper
{
    /// <summary>
    /// Represents the metadata associated to the API response.
    /// </summary>
    public class Meta
    {
        [JsonConstructor]
        internal Meta(int server_time, string server_timezone, int api_version, string execution_time)
        {
            this.ServerTime = server_time;
            this.ServerTimezone = server_timezone;
            this.ApiVersion = api_version;
            this.ExecutionTime = execution_time;
        }

        /// <summary>
        /// Gets the server time at the time of execution.
        /// </summary>
        public int ServerTime { get; private set; }
        /// <summary>
        /// Gets the server timezone.
        /// </summary>
        public string ServerTimezone { get; private set; }
        /// <summary>
        /// Gets the API version at the time of execution.
        /// </summary>
        public int ApiVersion { get; private set; }
        /// <summary>
        /// Gets the measured execution time.
        /// </summary>
        public string ExecutionTime { get; private set; }
    }

    internal class ApiResponseRaw
    {
        [JsonProperty(PropertyName = "status")]
        internal string Status { get; set; }
        [JsonProperty(PropertyName = "status_message")]
        internal string StatusMessage { get; set; }
        [JsonProperty(PropertyName = "data")]
        internal JObject Data { get; set; }
        [JsonProperty(PropertyName = "@meta")]
        internal Meta MetaData { get; set; }
    }

    /// <summary>
    /// Represents the API response.
    /// </summary>
    /// <typeparam name="T">The type of data that's going to be contained in the response.</typeparam>
    public class ApiResponse<T> where T : IDataModel
    {
        /// <summary>
        /// Gets the API response status.
        /// </summary>
        public string Status { get; private set; }
        /// <summary>
        /// Gets the API response status message.
        /// </summary>
        public string StatusMessage { get; private set; }
        /// <summary>
        /// Gets the API response data.
        /// </summary>
        public T Data { get; private set; }
        /// <summary>
        /// Gets the API response metadata.
        /// </summary>
        public Meta MetaData { get; private set; }

        internal ApiResponse(ApiResponseRaw rawResponse)
        {
            this.Status = rawResponse.Status;
            this.StatusMessage = rawResponse.StatusMessage;
            this.MetaData = rawResponse.MetaData;
            this.Data = rawResponse.Data.ToObject<T>();
        }
    }
}
