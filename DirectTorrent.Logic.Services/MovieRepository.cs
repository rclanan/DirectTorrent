using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DirectTorrent.Logic.Models;

namespace DirectTorrent.Logic.Services
{
    public static class MovieRepository
    {
        public static string GetMovie()
        {
            var temp = DirectTorrent.Data.Yify.ApiWrapper.ApiWrapper.ListMovies().Data.Movies[0];
            return temp.TitleLong;
        }
    }
}
