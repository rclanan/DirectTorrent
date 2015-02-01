using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;

namespace DirectTorrent.Data.Yify.Models
{
    public interface IDataModel
    {
    }

    #region Enums
    public enum Format
    {
        JSON,
        JSONP,
        XML
    }

    public enum Quality
    {
        HD,
        FHD,
        ThreeD,
        ALL
    }

    public enum Sort
    {
        Title,
        Year,
        Rating,
        Peers,
        Seeds,
        DownloadCount,
        LikeCount,
        DateAdded
    }

    public enum Order
    {
        Ascending,
        Descending
    }
    #endregion

    public class Torrent : ICloneable
    {

        public Uri Url { get; private set; }
        public string Hash { get; private set; }
        public Quality Quality { get; private set; }
        public int Seeds { get; private set; }
        public int Peers { get; private set; }
        public string Size { get; private set; }
        public int SizeBytes { get; private set; }
        public DateTime DateUploaded { get; private set; }
        public int DateUploadedUnix { get; private set; }

        [JsonConstructor]
        internal Torrent(string url, string hash, string quality, int seeds, int peers, string size, int size_bytes,
            string date_uploaded, int date_uploaded_unix)
        {
            this.Url = new Uri(url);
            this.Hash = hash;
            this.Quality = ParseQuality(quality);
            this.Seeds = seeds;
            this.Peers = peers;
            this.Size = size;
            this.SizeBytes = size_bytes;
            this.DateUploaded = DateTime.Parse(date_uploaded);
            this.DateUploadedUnix = date_uploaded_unix;
        }

        private Quality ParseQuality(string qual)
        {
            switch (qual)
            {
                case "3D":
                    return Quality.ThreeD;
                case "1080p":
                    return  Quality.FHD;
                case "720p":
                    return Quality.HD;
            }
            throw new ArgumentOutOfRangeException("qual");
        }

        public object Clone()
        {
            Torrent temp = (Torrent)this.MemberwiseClone();
            temp.Url = new Uri(this.Url.OriginalString);

            return (object) temp;
        }
    }

    public class Movie : ICloneable
    {

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

        [JsonConstructor]
        internal Movie(int id, string url, string imdb_code, string title, string title_long, int year, double rating,
            int runtime, List<string> genres, string language, string mpa_rating, string small_cover_image,
            string medium_cover_image, string state, List<Torrent> torrents, string date_uploaded,
            int date_uploaded_unix)
        {
            this.Id = id;
            this.Url = new Uri(url);
            this.ImdbCode = imdb_code;
            this.Title = title;
            this.TitleLong = title_long;
            this.Year = year;
            this.Rating = rating;
            this.Runtime = runtime;
            this.Genres = genres;
            this.Language = language;
            this.MpaRating = mpa_rating;
            // TODO: Convert to images?
            this.SmallCoverImage = small_cover_image;
            this.MediumCoverImage = medium_cover_image;
            this.State = state;
            this.Torrents = torrents.Select(x => (Torrent)x.Clone()).ToList();
            this.DateUploaded = DateTime.Parse(date_uploaded);
            this.DateUploadedUnix = date_uploaded_unix;
        }

        public object Clone()
        {
            Movie temp = (Movie)this.MemberwiseClone();
            temp.Url = new Uri(this.Url.OriginalString);
            temp.Genres = new List<string>(this.Genres);
            temp.Torrents = this.Torrents.Select(x => (Torrent) x.Clone()).ToList();
            return (object) temp;

        }
    }

    public class ListMoviesData : IDataModel
    {
        public int MovieCount { get; private set; }
        public int Limit { get; private set; }
        public int PageNumber { get; private set; }
        public List<Movie> Movies { get; private set; }

        [JsonConstructor]
        internal ListMoviesData(int movie_count, int limit, int page_number, List<Movie> movies)
        {
            this.MovieCount = movie_count;
            this.Limit = limit;
            this.PageNumber = page_number;
            // TODO: Deep copy
            this.Movies = movies;
        }
    }

    public class UpcomingMoviesData : IDataModel
    {
        //._. TODO: When there's data to model
    }

    public class Director : ICloneable
    {
        public string Name { get; private set; }
        public string SmallImage { get; private set; }
        public string MediumImage { get; private set; }

        [JsonConstructor]
        internal Director(string name, string small_image, string medium_image)
        {
            this.Name = name;
            // TODO: Convert to images?
            this.SmallImage = small_image;
            this.MediumImage = medium_image;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class Actor : ICloneable
    {
        public string Name { get; private set; }
        public string CharacterName { get; private set; }
        public string SmallImage { get; private set; }
        public string MediumImage { get; private set; }

        [JsonConstructor]
        internal Actor(string name, string character_name, string small_image, string medium_image)
        {
            this.Name = name;
            this.CharacterName = character_name;
            // TODO: Convert to images?
            this.SmallImage = small_image;
            this.MediumImage = medium_image;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class Images
    {
        public string BackgroundImage { get; private set; }
        public string SmallCoverImage { get; private set; }
        public string MediumCoverImage { get; private set; }
        public string LargeCoverImage { get; private set; }
        public string MediumScreenshotImage1 { get; private set; }
        public string MediumScreenshotImage2 { get; private set; }
        public string MediumScreenshotImage3 { get; private set; }
        public string LargeScreenshotImage1 { get; private set; }
        public string LargeScreenshotImage2 { get; private set; }
        public string LargeScreenshotImage3 { get; private set; }

        [JsonConstructor]
        internal Images(string background_image, string small_cover_image, string medium_cover_image,
            string large_cover_image, string medium_screenshot_image1, string medium_screenshot_image2,
            string medium_screenshot_image3, string large_screenshot_image1, string large_screenshot_image2,
            string large_screenshot_image3)
        {
            // TODO: Convert to images?
            this.BackgroundImage = background_image;
            this.SmallCoverImage = small_cover_image;
            this.LargeCoverImage = large_cover_image;
            this.MediumCoverImage = medium_cover_image;
            this.MediumScreenshotImage1 = medium_screenshot_image1;
            this.MediumScreenshotImage2 = medium_screenshot_image2;
            this.MediumScreenshotImage3 = medium_screenshot_image3;
            this.LargeScreenshotImage1 = large_screenshot_image1;
            this.LargeScreenshotImage2 = large_screenshot_image2;
            this.LargeScreenshotImage3 = large_screenshot_image3;
        }
    }

    public class MovieDetailsData : IDataModel
    {
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
        public int DownloadCount { get; private set; }
        public int LikeCount { get; private set; }
        public int RtCriticsScore { get; private set; }
        public string RtCriticsRating { get; private set; }
        public int RtAudienceScore { get; private set; }
        public string RtAudienceRating { get; private set; }
        public string DescriptionIntro { get; private set; }
        public string DescriptionFull { get; private set; }
        public string YtTrailerCode { get; private set; }
        public DateTime DateUploaded { get; private set; }
        public int DateUploadedUnix { get; private set; }
        public Images Images { get; private set; }
        public List<Torrent> Torrents { get; private set; }
        public List<Director> Directors { get; private set; }
        public List<Actor> Actors { get; private set; }

        [JsonConstructor]
        internal MovieDetailsData(int id, string url, string imdb_code, string title, string title_long, int year,
            double rating, int runtime, List<string> genres, string language, string mpa_rating, int download_count,
            int like_count, int rt_critics_score, string rt_critics_rating, int rt_audience_score,
            string rt_audience_rating, string description_intro, string description_full, string yt_trailer_code,
            string date_uploaded, int date_uploaded_unix, Images images, List<Torrent> torrents,
            List<Director> directors, List<Actor> actors)
        {
            this.Id = id;
            this.Url = new Uri(url);
            this.ImdbCode = imdb_code;
            this.Title = title;
            this.TitleLong = title_long;
            this.Year = year;
            this.Rating = rating;
            this.Runtime = runtime;
            this.Genres = new List<string>(genres);
            this.Language = language;
            this.MpaRating = mpa_rating;
            this.DownloadCount = download_count;
            this.LikeCount = like_count;
            this.RtCriticsScore = rt_critics_score;
            this.RtCriticsRating = rt_critics_rating;
            this.RtAudienceScore = rt_audience_score;
            this.RtAudienceRating = rt_audience_rating;
            this.DescriptionIntro = description_intro;
            this.DescriptionFull = description_full;
            this.YtTrailerCode = yt_trailer_code;
            this.DateUploaded = DateTime.Parse(date_uploaded);
            this.DateUploadedUnix = date_uploaded_unix;
            this.Images = images;
            this.Torrents = torrents.Select(x => (Torrent)x.Clone()).ToList();
            this.Directors = directors;
            this.Actors = actors;
        }
    }
}