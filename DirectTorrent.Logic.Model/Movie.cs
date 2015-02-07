using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DirectTorrent.Data.Yify.Models;

namespace DirectTorrent.Logic.Models
{
    /// <summary>
    /// Represents the quality of the video.
    /// </summary>
    public enum Quality
    {
        HD,
        FHD,
        ThreeD,
        ALL
    }

    /// <summary>
    /// Represents the data associated to a torrent.
    /// </summary>
    public class Torrent
    {
        /// <summary>
        /// Gets the url of the torrent.
        /// </summary>
        public Uri Url { get; private set; }
        /// <summary>
        /// Gets the hash of the torrent.
        /// </summary>
        public string Hash { get; private set; }
        /// <summary>
        /// Gets the quality of the movie torrent.
        /// </summary>
        public Quality Quality { get; private set; }
        /// <summary>
        /// Gets the amount of seeds.
        /// </summary>
        public int Seeds { get; private set; }
        /// <summary>
        /// Gets the amount of peers.
        /// </summary>
        public int Peers { get; private set; }
        /// <summary>
        /// Gets the size of the movie.
        /// </summary>
        public string Size { get; private set; }
        /// <summary>
        /// Gets the size of the movie represented in bytes.
        /// </summary>
        public long SizeBytes { get; private set; }
        /// <summary>
        /// Gets the date when the torrent was uploaded.
        /// </summary>
        public DateTime DateUploaded { get; private set; }
        /// <summary>
        /// Gets the date when the torrent was uploaded as a unix timestamp.
        /// </summary>
        public int DateUploadedUnix { get; private set; }
    }

    /// <summary>
    /// Represents the data associated to the movie.
    /// </summary>
    public class Movie
    {
        /// <summary>
        /// Gets the ID of the movie.
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// Gets the url of the movie.
        /// </summary>
        public Uri Url { get; private set; }
        /// <summary>
        /// Gets the imdb code of the movie.
        /// </summary>
        public string ImdbCode { get; private set; }
        /// <summary>
        /// Gets the title of movie.
        /// </summary>
        public string Title { get; private set; }
        /// <summary>
        /// Gets the long title of the movie.
        /// </summary>
        public string TitleLong { get; private set; }
        /// <summary>
        /// Gets the release year of the movie.
        /// </summary>
        public int Year { get; private set; }
        /// <summary>
        /// Gets the movie rating.
        /// </summary>
        public double Rating { get; private set; }
        /// <summary>
        /// Gets the movie runtime.
        /// </summary>
        public int Runtime { get; private set; }
        /// <summary>
        /// Gets the list of genres to which the movie belongs.
        /// </summary>
        public List<string> Genres { get; private set; }
        /// <summary>
        /// Gets the language of the movie.
        /// </summary>
        public string Language { get; private set; }
        /// <summary>
        /// Gets the mpa rating of the movie.
        /// </summary>
        public string MpaRating { get; private set; }
        public string SmallCoverImage { get; private set; }
        public string MediumCoverImage { get; private set; }
        /// <summary>
        /// Gets the current movie state.
        /// </summary>
        public string State { get; private set; }
        /// <summary>
        /// Gets the list of torrents associated to the movie.
        /// </summary>
        public List<Torrent> Torrents { get; private set; }
        /// <summary>
        /// Gets the date when the movie was uploaded.
        /// </summary>
        public DateTime DateUploaded { get; private set; }
        /// <summary>
        /// Gets the date when the movie was uploaded as a unix timestamp.
        /// </summary>
        public int DateUploadedUnix { get; private set; }

        public Movie(Data.Yify.Models.Movie source)
        {
            //TODO: Write constructor, object-object mapper
            throw new NotImplementedException();
        }
    }
}
