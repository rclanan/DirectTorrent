using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DirectTorrent.Data.Yify.Models;

namespace DirectTorrent.Logic.Models
{
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
            Movie temp = null;
            AutoMapper.Mapper.CreateMap<Data.Yify.Models.Movie, Movie>();
            AutoMapper.Mapper.CreateMap<Data.Yify.Models.Torrent, Torrent>();
            temp = AutoMapper.Mapper.Map<Movie>(source);
            this.Id = temp.Id;
            this.Url = temp.Url;
            this.ImdbCode = temp.ImdbCode;
            this.Title = temp.Title;
            this.TitleLong = temp.TitleLong;
            this.Year = temp.Year;
            this.Rating = temp.Rating;
            this.Runtime = temp.Runtime;
            this.Genres = temp.Genres;
            this.Language = temp.Language;
            this.MpaRating = temp.MpaRating;
            this.SmallCoverImage = temp.SmallCoverImage;
            this.MediumCoverImage = temp.MediumCoverImage;
            this.State = temp.State;
            this.Torrents = temp.Torrents;
            this.DateUploaded = temp.DateUploaded;
            this.DateUploadedUnix = temp.DateUploadedUnix;
        }

        private Movie()
        {
            
        }
    }
}
