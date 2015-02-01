using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DirectTorrent.Data.ApiWrappers.Yify
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

        //[JsonProperty(PropertyName = "server_time")]
        public int ServerTime { get; private set; }
        //[JsonProperty(PropertyName = "server_timezone")]
        public string ServerTimezone { get; private set; }
        //[JsonProperty(PropertyName = "api_version")]
        public int ApiVersion { get; private set; }
        //[JsonProperty(PropertyName = "execution_time")]
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

    public class ApiResponse<T> where T : IApiDataModel
    {
        internal ApiResponse(ApiResponseRaw rawResponse)
        {
            this.Status = rawResponse.Status;
            this.StatusMessage = rawResponse.StatusMessage;
            this.MetaData = rawResponse.MetaData;
            this.Data = rawResponse.Data.ToObject<T>();
        }

        public string Status { get; private set; }
        public string StatusMessage { get; private set; }
        public T Data { get; private set; }
        public Meta MetaData { get; private set; }
    }
}
