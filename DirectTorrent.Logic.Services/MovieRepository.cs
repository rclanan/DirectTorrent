using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using DirectTorrent.Logic.Models;

namespace DirectTorrent.Logic.Services
{
    public static class MovieRepository
    {
        public static class Yify
        {
            public static string GetDummyData()
            {
                System.Diagnostics.Stopwatch timer = System.Diagnostics.Stopwatch.StartNew();
                var temp = DirectTorrent.Data.Yify.ApiWrapper.ApiWrapper.DummyMovieData().Data.Movies[0];
                Debug.WriteLine("Constructing movie cache: " + timer.Elapsed);
                timer.Stop();
                return temp.TitleLong;
            }

            public static string ListUpcomingMovies()
            {
                throw new NotImplementedException();
            }

            public static string GetMovieDetails()
            {
                throw new NotImplementedException();
            }

            public static string ListMovies()
            {
                throw new NotImplementedException();
            }
        }
    }
}
