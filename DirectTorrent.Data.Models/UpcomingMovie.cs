using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DirectTorrent.Data.Models
{
    class UpcomingMovie
    {
        public string MovieTitle { get; set; }
        public string MovieCover { get; set; }
        public string ImdbCode { get; set; }
        public string ImdbLink { get; set; }
        public string Uploader { get; set; }
        public string UploaderUID { get; set; }
        public string DateAdded { get; set; }
        public int DateAddedEpoch { get; set; }
    }
}
