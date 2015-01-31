using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Net.Sockets;

namespace DirectTorrent.Data.ApiWrappers
{
    public static class YifyWrapper
    {
        #region Enums

        // Enums for enforcing the value range of some api parameters
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
            Date,
            Seeds,
            Peers,
            Size,
            Alphabet,
            Rating,
            Downloaded,
            Year
        }

        public enum Order
        {
            Ascending,
            Descending
        }

        #endregion

        #region Enum Parsers

        // Parses the Format enum, for creating the api request string
        private static string ParseFormat(Format format)
        {
            switch (format)
            {
                case Format.JSON:
                    return "json";
                case Format.JSONP:
                    return "jsonp";
                case Format.XML:
                    return "xml";
            }
            throw new ArgumentOutOfRangeException("format");
        }

        // Parses the Quality enum, for creating the api request string
        private static string ParseQuality(Quality quality)
        {
            switch (quality)
            {
                case Quality.HD:
                    return "3D";
                case Quality.FHD:
                    return "1080p";
                case Quality.ThreeD:
                    return "3D";
                case Quality.ALL:
                    return "ALL";
            }
            throw new ArgumentOutOfRangeException("quality");
        }

        #endregion

        #region API Methods

        /// <summary>
        /// Provides an interface to the /api/upcoming yify API.
        /// </summary>
        /// <param name="format">Sets the format in which to display the results in. DO NOT USE ANYTHING OTHER THAN JSON! (Experimental)</param>
        /// <returns>A string representing the JSON response by the Yify API (upcoming movie releases).</returns>
        public static string GetUpcomingMovies(Format format = Format.JSON)
        {
            // Getting the response
            Stream stream;
            try
            {
                stream =
                    WebRequest.Create(string.Format("https://yts.re/api/upcoming.{0}", ParseFormat(format)))
                        .GetResponse()
                        .GetResponseStream();
            }
            catch (Exception)
            {
                // No internet connection
                throw new Exception("No internet connection.");
            }
            using (StreamReader sr = new StreamReader(stream))
            {
                // Checking if the response isn't empty
                string response = sr.ReadToEnd();
                if (!response.Contains("No upcoming movies"))
                    return response;
                throw new Exception("No upcoming movies.");
            }
        }

        /// <summary>
        /// Provides an interface to the /api/list yify API.
        /// </summary>
        /// <param name="format">Sets the format in which to display the results in. DO NOT USE ANYTHING OTHER THAN JSON! (Experimental)</param>
        /// <param name="limit">Sets the max amount of movie results (1-50, inclusive).</param>
        /// <param name="set">Sets the set of movies to display (eg limit=15 and set=2 will show you movies 15-30).</param>
        /// <param name="quality">Sets a quality type to filter by.</param>
        /// <param name="rating">Sets minimum movie rating for display (0-9, inclusive).</param>
        /// <param name="keywords">Sets the keywords to search by (maybe be multiple keywords, eg. britney, spears).</param>
        /// <param name="genre">Sets the genre from which to display movies from.</param>
        /// <param name="sort">Sets the sorting parameter.</param>
        /// <param name="order">Sets the order in which the movies will be displayed.</param>
        /// <exception cref="ArgumentOutOfRangeException">Limit or rating values were out of range.</exception>
        /// <returns>A string representing the JSON response by the Yify API (the query result).</returns>
        public static string ListMovies(Format format = Format.JSON, byte limit = 20, byte set = 1,
            Quality quality = Quality.ALL, byte rating = 0, string keywords = "", string genre = "ALL",
            Sort sort = Sort.Date, Order order = Order.Descending)
        {
            // Parameter value range checking
            if (limit > 50 || limit < 1)
                throw new ArgumentOutOfRangeException("limit", limit, "Must be between 1 - 50 (inclusive).");
            if (rating > 9)
                throw new ArgumentOutOfRangeException("rating", rating, "Must be between 0 - 9 (inclusive).");
            // Forming the request string
            string apiReq =
                string.Format("limit={0}&set={1}&quality={2}&rating={3}&keywords={4}&genre={5}&sort={6}&order={7}",
                    limit, set, ParseQuality(quality), rating, keywords, genre, sort.ToString().ToLower(),
                    order.ToString().ToLower().Substring(0, 3));
            // Getting the response
            Stream stream;
            try
            {
                stream =
                    WebRequest.Create(string.Format("https://yts.re/api/list.{0}?{1}", ParseFormat(format), apiReq))
                        .GetResponse()
                        .GetResponseStream();
            }
            catch (WebException ex)
            {
                // No internet connection
                throw new Exception("No internet connection.");
            }
            using (StreamReader sr = new StreamReader(stream))
            {
                // Checking if the response isn't empty
                string response = sr.ReadToEnd();
                if (!response.Contains("No movies found"))
                    return response;
                throw new Exception("No movies found.");
            }
        }

        /// <summary>
        /// Provides an interface to the /api/listimdb yify API.
        /// </summary>
        /// <param name="format">Sets the format in which to display the results in. DO NOT USE ANYTHING OTHER THAN JSON! (Experimental)</param>
        /// <param name="imdbIds">Sets the IMDB ID(s)s of the movis(s) that are/is to be retreived.</param>
        /// <returns>A string representing the JSON response by the Yify API (the query result).</returns>
        public static string ListMoviesByImdb(Format format = Format.JSON, params int[] imdbIds)
        {
            // Parameter value checking
            if (imdbIds.Length > 50) throw new ArgumentOutOfRangeException("imdbIds", imdbIds, "Must be less than 50.");
            // Building the api request string
            StringBuilder apiReq = new StringBuilder();
            foreach (var imdbId in imdbIds)
            {
                // Imdb code is in 7 digit format, adding the padding (lead zeros)
                apiReq.AppendFormat("imdb_id[]=tt{0}&", imdbId.ToString("D7"));
            }
            // Removing the last "&"
            apiReq.Remove(apiReq.Length - 1, 1);
            // Getting the response
            Stream stream;
            try
            {
                stream =
                    WebRequest.Create(string.Format("https://yts.re/api/listimdb.{0}?{1}", ParseFormat(format), apiReq))
                        .GetResponse()
                        .GetResponseStream();
            }
            catch (Exception)
            {
                // No internet connection
                throw new Exception("No internet connection.");
            }
            using (StreamReader sr = new StreamReader(stream))
            {
                // Checking if the response isn't empty
                string response = sr.ReadToEnd();
                if (!response.Contains("No movies found"))
                    return response;
                throw new Exception("No movies found.");
            }
        }

        /// <summary>
        /// Provides an interface to the /api/movie yify API.
        /// </summary>
        /// <param name="movieId">Sets the id of the movie details which are to be queried.</param>
        /// <param name="format">Sets the format in which to display the results in. DO NOT USE ANYTHING OTHER THAN JSON! (Experimental)</param>
        /// <returns>A string representing the JSON response by the Yify API (queried movie details).</returns>
        public static string GetMovieDetails(int movieId, Format format = Format.JSON)
        {
            // Getting the response
            Stream stream;
            try
            {
                stream =
                    WebRequest.Create(string.Format("https://yts.re/api/movie.{0}?id={1}", ParseFormat(format), movieId))
                        .GetResponse()
                        .GetResponseStream();
            }
            catch (Exception)
            {
                // No internet connection
                throw new Exception("No internet connection.");
            }
            using (StreamReader sr = new StreamReader(stream))
            {
                // Checking if the response isn't empty
                string response = sr.ReadToEnd();
                if (!response.Contains("No movies found"))
                    return response;
                throw new Exception("No movies found.");
            }
        }

        #endregion
    }
}