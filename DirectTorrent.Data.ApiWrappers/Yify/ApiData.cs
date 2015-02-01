using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace DirectTorrent.Data.ApiWrappers.Yify
{
    public interface IApiDataModel{}

    public class Torrent
    {
        [JsonConstructor]
        internal Torrent(string url, string hash, string quality, int seeds, int peers, string size, int size_bytes,
            string date_uploaded, int date_uploaded_unix)
        {
            this.Url = new Uri(url);
            this.Hash = hash;
            this.Quality = quality; //TODO: Add enum
            this.Seeds = seeds;
            this.Peers = peers;
            this.Size = size;
            this.SizeBytes = size_bytes;
            this.DateUploaded = DateTime.Parse(date_uploaded);
            this.DateUploadedUnix = date_uploaded_unix;
        }
        public Uri Url { get; private set; }
        public string Hash { get; private set; }
        public string Quality { get; private set; }
        public int Seeds { get; private set; }
        public int Peers { get; private set; }
        public string Size { get; private set; }
        public int SizeBytes { get; private set; }
        public DateTime DateUploaded { get; private set; }
        public int DateUploadedUnix { get; private set; }
    }

    public class Movie
    {
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
            // TODO: Add enum
            this.State = state;
            // TODO: Deep copy
            this.Torrents = torrents;
            this.DateUploaded = DateTime.Parse(date_uploaded);
            this.DateUploadedUnix = date_uploaded_unix;
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

    public class ListMoviesData : IApiDataModel
    {
        [JsonConstructor]
        internal ListMoviesData(int movie_count, int limit, int page_number, List<Movie> movies)
        {
            this.MovieCount = movie_count;
            this.Limit = limit;
            this.PageNumber = page_number;
            // TODO: Deep copy
            this.Movies = movies;
        }

        public int MovieCount { get; private set; }
        public int Limit { get; private set; }
        public int PageNumber { get; private set; }
        public List<Movie> Movies { get; private set; }
    }

    public class UpcomingMoviesData : IApiDataModel
    {
        //._. TODO: When there's data to model
    }

    public class Director
    {
        [JsonConstructor]
        internal Director(string name, string small_image, string medium_image)
        {
            this.Name = name;
            // TODO: Convert to images?
            this.SmallImage = small_image;
            this.MediumImage = medium_image;
        }

        public string Name { get; private set; }
        public string SmallImage { get; private set; }
        public string MediumImage { get; private set; }
    }

    public class Actor
    {
        [JsonConstructor]
        internal Actor(string name, string character_name, string small_image, string medium_image)
        {
            this.Name = name;
            this.CharacterName = character_name;
            // TODO: Convert to images?
            this.SmallImage = small_image;
            this.MediumImage = medium_image;
        }

        public string Name { get; private set; }
        public string CharacterName { get; private set; }
        public string SmallImage { get; private set; }
        public string MediumImage { get; private set; }
    }

    public class Images
    {
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
            this.LargeScreenshotImage1 = LargeScreenshotImage1;
            this.LargeScreenshotImage2 = LargeScreenshotImage2;
            this.LargeScreenshotImage3 = large_screenshot_image3;
        }

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
    }

    public class MovieDetailsData : IApiDataModel
    {
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
            // TODO: Deep copy
            this.Genres = genres;
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
            // TODO: Deep copy
            this.Images = images;
            this.Torrents = torrents;
            this.Directors = directors;
            this.Actors = actors;
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
    } 
}
