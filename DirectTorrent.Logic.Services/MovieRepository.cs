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
using Quality = DirectTorrent.Data.Yify.Models.Quality;

namespace DirectTorrent.Logic.Services
{
    public static class MovieRepository
    {
        public static class Yify
        {
            public static string GetDummyData()
            {
                System.Diagnostics.Stopwatch timer = System.Diagnostics.Stopwatch.StartNew();
                var temp = DirectTorrent.Data.Yify.ApiWrapper.ApiWrapper.DummyMovieData().Data.Movies[14];
                Debug.WriteLine("Constructing movie cache: " + timer.Elapsed);
                timer.Stop();
                return temp.TitleLong;
            }

            public static List<UpcomingMovie> ListUpcomingMovies()
            {
                var temp = new List<UpcomingMovie>();
                var source = ApiWrapper.ListUpcomingMovies();
                source.Data.UpcomingMovies.ForEach(x =>
                {
                    var tempMov = new UpcomingMovie(x);
                    temp.Add(tempMov);
                });

                return temp;
            }

            public static MovieDetails GetMovieDetails(int movieId)
            {
                MovieDetailsData temp = ApiWrapper.GetMovieDetails(movieId).Data;
                return new MovieDetails(temp);
            }

            public static List<Movie> ListMovies(byte limit = 20, uint page = 1,
                Quality quality = Quality.ALL, byte minimumRating = 0, string queryTerm = "", string genre = "ALL",
                Sort sortBy = Sort.DateAdded, Order orderBy = Order.Descending)
            {
                var temp = new List<Movie>();
                var source = ApiWrapper.ListMovies(Format.JSON, limit, page, quality, minimumRating, queryTerm, genre,
                    sortBy, orderBy);
                source.Data.Movies.ForEach(x =>
                {
                    var tempMov = new Movie(x);
                    temp.Add(tempMov);
                });
                return temp;
            }
        }
    }
}