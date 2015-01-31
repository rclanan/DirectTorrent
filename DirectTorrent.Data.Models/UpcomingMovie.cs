using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DirectTorrent.Data.ApiWrappers;
using Newtonsoft.Json;

namespace DirectTorrent.Data.Models
{
    class UpcomingMovie
    {
        #region Properties
        public string MovieTitle { get; set; }
        public string MovieCover { get; set; }
        public string ImdbCode { get; set; }
        public string ImdbLink { get; set; }
        public string Uploader { get; set; }
        public string UploaderUID { get; set; }
        public string DateAdded { get; set; }
        public int DateAddedEpoch { get; set; }
        #endregion

        public static List<UpcomingMovie> GetUpcomingMovies()
        {
            return JsonConvert.DeserializeObject<List<UpcomingMovie>>(YifyWrapper.GetUpcomingMovies());
        }
    }
}
