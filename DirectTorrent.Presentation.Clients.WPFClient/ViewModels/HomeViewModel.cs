using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using DirectTorrent.Logic.Services;
using FirstFloor.ModernUI.Presentation;

namespace DirectTorrent.Presentation.Clients.WPFClient.ViewModels
{
    public class HomeViewModel : NotifyPropertyChanged
    {
        public ObservableCollection<MovieItem> listaFilmova { get; private set; }

        public HomeViewModel()
        {
            listaFilmova = new ObservableCollection<MovieItem>();
            MovieRepository.Yify.ListMovies().ForEach(x => listaFilmova.Add(new MovieItem(x)));
        }
    }

    public class MovieItem
    {
        public BitmapImage Image { get; private set; }
        public string Name { get; private set; }
        public int Year { get; private set; }
        public MovieItem(Logic.Models.Movie movie)
        {
            this.Image = new BitmapImage(new Uri(movie.MediumCoverImage));
            this.Name = movie.Title;
            this.Year = movie.Year;
        }
    }
}
