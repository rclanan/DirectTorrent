using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.IO;

namespace DirectTorrent.Data.ApiWrappers
{
    public class YifyWrapper
    {
        public string Map()
        {
            WebRequest request = WebRequest.Create(
            "https://yts.re/api/list.json");
            request.Proxy = null;
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            response.Close();
            return responseFromServer;
        }
    }
}
