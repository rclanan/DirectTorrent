using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.IO;

namespace DirectTorrent.Data.ApiWrappers
{
    public class YifyWrapper
    {
        public enum Format { JSON, JSONP, XML }
        public enum Quality { HD, FHD, ThreeD, ALL }
        public enum Sort { Date, Seeds, Peers, Size, Alphabet, Rating, Downloaded, Year }
        public enum Order { Ascending, Descending }

        /// <summary>
        /// Provides an interface to the /api/list yify API.
        /// </summary>
        /// <param name="format">Sets the format in which to display the results in.</param>
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
        public string ListMovies(Format format = Format.JSON, byte limit = 20, byte set = 1, Quality quality = Quality.ALL, byte rating = 0, string keywords = "", string genre = "ALL", Sort sort = Sort.Date, Order order = Order.Descending)
        {
            if (limit > 50 || limit < 1) throw new ArgumentOutOfRangeException("limit", limit, "Must be between 1 - 50 (inclusive).");
            if (rating > 9) throw new ArgumentOutOfRangeException("rating", rating, "Must be between 0 - 9 (inclusive).");
            string qual = null;
            string form = null;
            switch (quality)
            {
                case Quality.HD:
                    qual = "3D";
                    break;
                case Quality.FHD:
                    qual = "1080p";
                    break;
                case Quality.ThreeD:
                    qual = "3D";
                    break;
                case Quality.ALL:
                    qual = "ALL";
                    break;
            }
            switch (format)
            {
                case Format.JSON:
                    form = "json";
                    break;
                case Format.JSONP:
                    form = "jsonp";
                    break;
                case Format.XML:
                    form = "xml";
                    break;

            }
            string apiReq = string.Format("limit={0}&set={1}&quality={2}&rating={3}&keywords={4}&genre={5}&sort={6}&order={7}", limit, set, qual, rating, keywords, genre, sort.ToString().ToLower(), order.ToString().ToLower().Substring(0, 3));
            using (StreamReader sr = new StreamReader(WebRequest.Create(string.Format("https://yts.re/api/list.{0}?{1}", form, apiReq)).GetResponse().GetResponseStream()))
            {
                return sr.ReadToEnd();
            }
        }


        public string ListMoviesByImdb(params int[] imdb_ids)
        {
            throw new NotImplementedException();
        }
    }
}