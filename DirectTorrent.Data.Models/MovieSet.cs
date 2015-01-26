using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DirectTorrent.Data.Models
{
    public class MovieSet
    {
        public int MovieCount { get; set; }
        public List<Movie> MovieList { get; set; }
    }
}
