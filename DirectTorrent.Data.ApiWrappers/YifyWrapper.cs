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
        public string ListAllMovies()
        {
            using (StreamReader sr = new StreamReader(WebRequest.Create("https://yts.re/api/list.json").GetResponse().GetResponseStream()))
            {
                return sr.ReadToEnd();
            }
        }
    }
}
