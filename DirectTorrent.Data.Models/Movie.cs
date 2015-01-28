using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using System.Net;
using System.IO;
using DirectTorrent.Data.ApiWrappers;
using Newtonsoft.Json;

namespace DirectTorrent.Data.Models
{
    public class Movie
    {
        #region Properties
        public int MovieID { get; set; }
        public string State { get; set; }
        public string MovieUrl { get; set; }
        public string MovieTitle { get; set; }
        public string MovieTitleClean { get; set; }
        public int MovieYear { get; set; }
        public string AgeRating { get; set; }
        public string DateUploaded { get; set; }
        public int DateUploadedEpoch { get; set; }
        public string Quality { get; set; }
        public string CoverImage { get; set; }
        public string ImdbCode { get; set; }
        public string ImdbLink { get; set; }
        public string Size { get; set; }
        public long SizeByte { get; set; }
        public float MovieRating { get; set; }
        public string Genre { get; set; }
        public string Uploader { get; set; }
        public int UploaderUID { get; set; }
        public int Downloaded { get; set; }
        public int TorrentSeeds { get; set; }
        public int TorrentPeers { get; set; }
        public string TorrentUrl { get; set; }
        public string TorrentHash { get; set; }
        public string TorrentMagnetUrl { get; set; }
        #endregion

        public static List<Movie> PopulateModel()
        {
            return JsonConvert.DeserializeObject<MovieSet>(new YifyWrapper().ListAllMovies()).MovieList;
        }
    }
}