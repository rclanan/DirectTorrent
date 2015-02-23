using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DirectTorrent.Logic.Models
{
    /// <summary>
    /// Represents the data associated to the movie, contains details.
    /// </summary>
    public class MovieDetails
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
        /// Gets the title of the movie.
        /// </summary>
        public string Title { get; private set; }
        /// <summary>
        /// Gets the long title of the movie.
        /// </summary>
        public string TitleLong { get; private set; }
        /// <summary>
        /// Gets the year in which the movie was released.
        /// </summary>
        public int Year { get; private set; }
        /// <summary>
        /// Gets the rating of the movie.
        /// </summary>
        public double Rating { get; private set; }
        /// <summary>
        /// Gets the runtime of the movie.
        /// </summary>
        public int Runtime { get; private set; }
        /// <summary>
        /// Gets the genres to which the movie belongs.
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
        /// <summary>
        /// Gets the download count of the movie.
        /// </summary>
        public int DownloadCount { get; private set; }
        /// <summary>
        /// Gets the like count of the movie.
        /// </summary>
        public int LikeCount { get; private set; }
        /// <summary>
        /// Gets the RottenTomatoes critics score.
        /// </summary>
        public int RtCriticsScore { get; private set; }
        /// <summary>
        /// Gets the RottenTomatoes critics rating.
        /// </summary>
        public string RtCriticsRating { get; private set; }
        /// <summary>
        /// Gets the RottenTomatoes audience score.
        /// </summary>
        public int RtAudienceScore { get; private set; }
        /// <summary>
        /// Gets the RottenTomatoes audience rating.
        /// </summary>
        public string RtAudienceRating { get; private set; }
        /// <summary>
        /// Gets the abstract of the description.
        /// </summary>
        public string DescriptionIntro { get; private set; }
        /// <summary>
        /// Gets the full description of the movie.
        /// </summary>
        public string DescriptionFull { get; private set; }
        /// <summary>
        /// Gets the YouTube trailer code of the movie.
        /// </summary>
        public string YtTrailerCode { get; private set; }
        /// <summary>
        /// Gets the date when the movie was uploaded.
        /// </summary>
        public DateTime DateUploaded { get; private set; }
        /// <summary>
        /// Gets the date when the movie was uploaded as a unix timestamp.
        /// </summary>
        public int DateUploadedUnix { get; private set; }
        /// <summary>
        /// Gets the set of images associated to the movie.
        /// </summary>
        public Images Images { get; private set; }
        /// <summary>
        /// Gets the list of torrents associated to the movie.
        /// </summary>
        public List<Torrent> Torrents { get; private set; }
        /// <summary>
        /// Gets the list of directors associated to the movie.
        /// </summary>
        public List<Director> Directors { get; private set; }
        /// <summary>
        /// Gets the list of actors associated to the movie.
        /// </summary>
        public List<Actor> Actors { get; private set; }

        public MovieDetails(DirectTorrent.Data.Yify.Models.MovieDetailsData source)
        {
            MovieDetails temp = null;
            AutoMapper.Mapper.CreateMap<Data.Yify.Models.Torrent, Torrent>();
            AutoMapper.Mapper.CreateMap<Data.Yify.Models.Actor, Actor>();
            AutoMapper.Mapper.CreateMap<Data.Yify.Models.Director, Director>();
            AutoMapper.Mapper.CreateMap<Data.Yify.Models.Images, Images>();
            AutoMapper.Mapper.CreateMap<Data.Yify.Models.MovieDetailsData, MovieDetails>();
            temp = AutoMapper.Mapper.Map<MovieDetails>(source);
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
            this.DownloadCount = temp.DownloadCount;
            this.LikeCount = temp.LikeCount;
            this.RtCriticsScore = temp.RtCriticsScore;
            this.RtCriticsRating = temp.RtCriticsRating;
            this.RtAudienceScore = temp.RtAudienceScore;
            this.RtAudienceRating = temp.RtAudienceRating;
            this.DescriptionIntro = temp.DescriptionIntro;
            this.DescriptionFull = temp.DescriptionFull;
            this.YtTrailerCode = temp.YtTrailerCode;
            this.DateUploaded = temp.DateUploaded;
            this.DateUploadedUnix = temp.DateUploadedUnix;
            this.Images = temp.Images;
            this.Torrents = temp.Torrents;
            this.Directors = temp.Directors;
            this.Actors = temp.Actors;
        }

        private MovieDetails()
        {
        }
    }
}
