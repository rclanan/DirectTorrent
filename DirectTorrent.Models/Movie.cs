using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DirectTorrent.Models
{
    public class Movie
    {
        public string MovieID { get; set; }
        public string State { get; set; }
        public string MovieUrl { get; set; }
        public string MovieTitle { get; set; }
        public string MovieTitleClean { get; set; }
        public string MovieYear { get; set; }
        public string AgeRating { get; set; }
        public string DateUploaded { get; set; }
        public int DateUploadedEpoch { get; set; }
        public string Quality { get; set; }
        public string CoverImage { get; set; }
        public string ImdbCode { get; set; }
        public string ImdbLink { get; set; }
        public string Size { get; set; }
        public string SizeByte { get; set; }
        public string MovieRating { get; set; }
        public string Genre { get; set; }
        public string Uploader { get; set; }
        public string UploaderUID { get; set; }
        public string Downloaded { get; set; }
        public string TorrentSeeds { get; set; }
        public string TorrentPeers { get; set; }
        public string TorrentUrl { get; set; }
        public string TorrentHash { get; set; }
        public string TorrentMagnetUrl { get; set; }
    }
}
