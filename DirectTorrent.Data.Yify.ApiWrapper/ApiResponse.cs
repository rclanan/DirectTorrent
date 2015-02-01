using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DirectTorrent.Data.Yify.Models;

namespace DirectTorrent.Data.Yify.ApiWrapper
{
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

        public int ServerTime { get; private set; }
        public string ServerTimezone { get; private set; }
        public int ApiVersion { get; private set; }
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

    public class ApiResponse<T> where T : IDataModel
    {

        public string Status { get; private set; }
        public string StatusMessage { get; private set; }
        public T Data { get; private set; }
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
