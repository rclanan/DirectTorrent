using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using DirectTorrent.Models;
using System.Net;

namespace DirectTorrent.Data.Mappers
{
    public class YifyMapper
    {
        public List<Movie>  void Map()
        {
            WebRequest request = WebRequest.Create(
            "https://yts.re/api/list.json");
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            Console.WriteLine(responseFromServer);
            reader.Close();
            response.Close();
            Movies filmovi = JsonConvert.DeserializeObject<Movies>(responseFromServer);
            return filmovi;
        }
    }
}
