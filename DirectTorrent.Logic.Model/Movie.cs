using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DirectTorrent.Logic.Models
{
    public class Movie
    {
        public class Torrent
        {
            public enum Quality
            {
                HD,
                FHD,
                ThreeD,
                ALL
            }

            public Uri Url { get; private set; }
            public string Hash { get; private set; }
            public Quality MovieQuality { get; private set; }
            public int Seeds { get; private set; }
            public int Peers { get; private set; }
            public string Size { get; private set; }
            public long SizeBytes { get; private set; }
            public DateTime DateUploaded { get; private set; }
            public int DateUploadedUnix { get; private set; }
        }

        public int Id { get; private set; }
        public Uri Url { get; private set; }
        public string ImdbCode { get; private set; }
        public string Title { get; private set; }
        public string TitleLong { get; private set; }
        public int Year { get; private set; }
        public double Rating { get; private set; }
        public int Runtime { get; private set; }
        public List<string> Genres { get; private set; }
        public string Language { get; private set; }
        public string MpaRating { get; private set; }
        public string SmallCoverImage { get; private set; }
        public string MediumCoverImage { get; private set; }
        public string State { get; private set; }
        public List<Torrent> Torrents { get; private set; }
        public DateTime DateUploaded { get; private set; }
        public int DateUploadedUnix { get; private set; }
    }
}
