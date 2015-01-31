using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Net;
using System.IO;
using DirectTorrent.Data.ApiWrappers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DirectTorrent.Data.Models
{
    public class MovieSet
    {
        public int MovieCount { get; private set; }
        public List<Movie> MovieList { get; private set; }

        [JsonConstructor]
        private MovieSet(int movieCount, List<Movie> movieList)
        {
            this.MovieCount = movieCount;
            this.MovieList = movieList;
        }

        public MovieSet(YifyWrapper.Format format = YifyWrapper.Format.JSON, byte limit = 20, byte set = 1,
            YifyWrapper.Quality quality = YifyWrapper.Quality.ALL, byte rating = 0, string keywords = "",
            string genre = "ALL",
            YifyWrapper.Sort sort = YifyWrapper.Sort.Date, YifyWrapper.Order order = YifyWrapper.Order.Descending)
        {
            string resp = YifyWrapper.ListMovies(format, limit, set, quality, rating, keywords, genre, sort, order);
            var temp =JsonConvert.DeserializeObject<MovieSet>(resp);
            this.MovieCount = temp.MovieCount;
            this.MovieList = temp.MovieList.Select(x => (Movie) x.Clone()).ToList();
        }
    }

    public class Movie : ICloneable
    {
        #region Properties

        public ushort MovieID { get; private set; }
        public string State { get; private set; }
        public string MovieUrl { get; private set; }
        public string MovieTitle { get; private set; }
        public string MovieTitleClean { get; private set; }
        public ushort MovieYear { get; private set; }
        public string AgeRating { get; private set; }
        public string DateUploaded { get; private set; }
        public uint DateUploadedEpoch { get; private set; }
        public string Quality { get; private set; }
        public string CoverImage { get; private set; }
        public string ImdbCode { get; private set; }
        public string ImdbLink { get; private set; }
        public string Size { get; private set; }
        public uint SizeByte { get; private set; }
        public float MovieRating { get; private set; }
        public string Genre { get; private set; }
        public string Uploader { get; private set; }
        public uint UploaderUID { get; private set; }
        public uint Downloaded { get; private set; }
        public ushort TorrentSeeds { get; private set; }
        public ushort TorrentPeers { get; private set; }
        public string TorrentUrl { get; private set; }
        public string TorrentHash { get; private set; }
        public string TorrentMagnetUrl { get; private set; }

        #endregion

        [JsonConstructor]
        public Movie(ushort movieId, string state, string movieUrl, string movieTitle, string movieTitleClean, ushort movieYear,
            string ageRating, string dateUploaded, uint dateUploadedEpoch, string quality, string coverImage,
            string imdbCode, string imdbLink, string size, uint sizeByte, float movieRating, string genre,
            string uploader, uint uploaderUid, uint downloaded, ushort torrentSeeds, ushort torrentPeers, string torrentUrl,
            string torrentHash, string torrentMagnet)
        {
            MovieID = movieId;
            State = state;
            MovieUrl = movieUrl;
            MovieTitle = movieTitle;
            MovieTitleClean = movieTitleClean;
            MovieYear = movieYear;
            AgeRating = ageRating;
            DateUploaded = dateUploaded;
            DateUploadedEpoch = dateUploadedEpoch;
            Quality = quality;
            CoverImage = coverImage;
            ImdbCode = imdbCode;
            ImdbLink = imdbLink;
            Size = size;
            SizeByte = sizeByte;
            MovieRating = movieRating;
            Genre = genre;
            Uploader = uploader;
            UploaderUID = uploaderUid;
            Downloaded = downloaded;
            TorrentSeeds = torrentSeeds;
            TorrentPeers = torrentPeers;
            TorrentUrl = torrentUrl;
            TorrentHash = torrentHash;
            TorrentMagnetUrl = torrentMagnet;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}