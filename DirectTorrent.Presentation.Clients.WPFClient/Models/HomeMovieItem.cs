using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DirectTorrent.Presentation.Clients.WPFClient.Models
{

    public class HomeMovieItem
    {
        public BitmapImage Image { get; private set; }
        public string Name { get; private set; }
        public int Year { get; private set; }
        public HomeMovieItem(Logic.Models.Movie movie)
        {
            this.Image = new BitmapImage(new Uri(movie.MediumCoverImage));
            this.Name = movie.Title;
            this.Year = movie.Year;
        }
    }
}
