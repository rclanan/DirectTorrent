using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Threading;

using DirectTorrent.Data.Yify.ApiWrapper;
using DirectTorrent.Data.Yify.Models;
using DirectTorrent.Logic.Models;
using UpcomingMovie = DirectTorrent.Logic.Models.UpcomingMovie;
using Movie = DirectTorrent.Logic.Models.Movie;
using Order = DirectTorrent.Data.Yify.Models.Order;
using Quality = DirectTorrent.Data.Yify.Models.Quality;
using Sort = DirectTorrent.Data.Yify.Models.Sort;

namespace DirectTorrent.Logic.Services
{
    // TODO: Additional work

    /// <summary>
    /// Represents the universal movie repository.
    /// </summary>
    public static class MovieRepository
    {
        /// <summary>
        /// Creates a torrent magnet uri.
        /// </summary>
        /// <param name="hash">The hash of the torrent.</param>
        /// <param name="movieName">The name of the movie.</param>
        /// <returns>The torrent magnet uri.</returns>
        public static string GetTorrentMagnetUri(string hash, string movieName)
        {
            return string.Format(
                "magnet:?xt=urn:btih:{0}&dn={1}&tr=udp://open.demonii.com:1337&tr=udp://tracker.istole.it:80&tr=http://tracker.yify-torrents.com/announce&tr=udp://tracker.publicbt.com:80&tr=udp://tracker.openbittorrent.com:80&tr=udp://tracker.coppersurfer.tk:6969&tr=udp://exodus.desync.com:6969&tr=http://exodus.desync.com:6969/announce",
                hash, System.Web.HttpUtility.UrlEncode(movieName));
        }

        /// <summary>
        /// Represents the Yify movie repository.
        /// </summary>
        public static class Yify
        {
            /// <summary>
            /// Provides dummy data for testing purposes.
            /// </summary>
            /// <returns>Dummy movie data for testing purposes.</returns>
            public static string GetDummyData()
            {
                System.Diagnostics.Stopwatch timer = System.Diagnostics.Stopwatch.StartNew();
                var temp = DirectTorrent.Data.Yify.ApiWrapper.ApiWrapper.DummyMovieData().Data.Movies;
                Debug.WriteLine("Constructing movie cache: " + timer.Elapsed);
                timer.Stop();
                return temp[0].TitleLong; // TODO: Something else?
            }

            /// <summary>
            /// Reads a list of upcoming movies on Yify.
            /// </summary>
            /// <returns>A list of upcoming movies.</returns>
            public static List<UpcomingMovie> ListUpcomingMovies()
            {
                var temp = new List<UpcomingMovie>();
                // Queries Yify for upcoming movies
                var source = ApiWrapper.ListUpcomingMovies();
                // Maps DTOs to business models
                source.Data.UpcomingMovies.ForEach(x =>
                {
                    var tempMov = new UpcomingMovie(x);
                    temp.Add(tempMov);
                });

                return temp;
            }

            /// <summary>
            /// Reads the movie details for based on a movie id.
            /// </summary>
            /// <param name="movieId">The id of the movie that's details should get queried.</param>
            /// <returns>A detailed set of information for the queried movie.</returns>
            public static MovieDetails GetMovieDetails(int movieId)
            {
                // Queries Yify for movie details
                MovieDetailsData temp = ApiWrapper.GetMovieDetails(movieId).Data;
                // Maps the DTO to the business model
                return new MovieDetails(temp);
            }

            /// <summary>
            /// Reads information about specific movies based on the query parameters.
            /// </summary>
            /// <param name="limit">The amount of movies to be returned by the query.</param>
            /// <param name="page">The set of movies to display (eg limit=15 and set=2 will show you movies 15-30).</param>
            /// <param name="quality">The quality type to filter by.</param>
            /// <param name="minimumRating">The minimum movie rating for display (0-9, inclusive).</param>
            /// <param name="queryTerm">The keywords to search by (maybe be multiple keywords, eg. britney, spears).</param>
            /// <param name="genre">The genre from which to display movies from.</param>
            /// <param name="sortBy">The sorting parameter.</param>
            /// <param name="orderBy">The order in which the movies will be displayed.</param>
            /// <returns>A list of movies that match the query parameters.</returns>
            public static List<Movie> ListMovies(byte limit = 20, uint page = 1,
                DirectTorrent.Logic.Models.Quality quality = DirectTorrent.Logic.Models.Quality.ALL, byte minimumRating = 0, string queryTerm = "", string genre = "ALL",
                DirectTorrent.Logic.Models.Sort sortBy = DirectTorrent.Logic.Models.Sort.DateAdded, DirectTorrent.Logic.Models.Order orderBy = DirectTorrent.Logic.Models.Order.Descending)
            {
                var temp = new List<Movie>();
                // Queries Yify for movies
                var source = ApiWrapper.ListMovies(Format.JSON, limit, page, qualityToQuality(quality), minimumRating, queryTerm, genre,
                    sortToSort(sortBy), orderToOrder(orderBy));
                // Maps DTOs to business models
                source.Data.Movies.ForEach(x =>
                {
                    var tempMov = new Movie(x);
                    temp.Add(tempMov);
                });
                return temp;
            }

            #region Enum Parsers
            private static DirectTorrent.Data.Yify.Models.Quality qualityToQuality(DirectTorrent.Logic.Models.Quality quality)
            {
                switch (quality)
                {
                    case Logic.Models.Quality.HD:
                        return Data.Yify.Models.Quality.HD;
                    case Logic.Models.Quality.FHD:
                        return Data.Yify.Models.Quality.FHD;
                    case Logic.Models.Quality.ALL:
                        return Data.Yify.Models.Quality.ALL;
                    case Logic.Models.Quality.ThreeD:
                        return Data.Yify.Models.Quality.ThreeD;

                }
                throw new ArgumentException("Quality is not valid.", "quality");
            }

            private static DirectTorrent.Data.Yify.Models.Sort sortToSort(DirectTorrent.Logic.Models.Sort sort)
            {
                switch (sort)
                {
                    case Logic.Models.Sort.Title:
                        return Data.Yify.Models.Sort.Title;
                    case Logic.Models.Sort.Year:
                        return Data.Yify.Models.Sort.Year;
                    case Logic.Models.Sort.Rating:
                        return Data.Yify.Models.Sort.Rating;
                    case Logic.Models.Sort.DownloadCount:
                        return Data.Yify.Models.Sort.DownloadCount;
                    case Logic.Models.Sort.DateAdded:
                        return Data.Yify.Models.Sort.DateAdded;
                }
                throw new ArgumentException("Sort is not valid.", "sort");

            }

            private static DirectTorrent.Data.Yify.Models.Order orderToOrder(DirectTorrent.Logic.Models.Order order)
            {
                switch (order)
                {
                    case Logic.Models.Order.Ascending:
                        return Data.Yify.Models.Order.Ascending;
                    case Logic.Models.Order.Descending:
                        return Data.Yify.Models.Order.Descending;
                }
                throw new ArgumentException("Order is valid.", "order");
            }
            #endregion Enum Parsers
        }
    }
}