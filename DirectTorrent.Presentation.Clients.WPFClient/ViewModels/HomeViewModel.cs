using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using DirectTorrent.Logic.Models;
using DirectTorrent.Logic.Services;

using DirectTorrent.Presentation.Clients.WPFClient.Models;
using FirstFloor.ModernUI.Presentation;

namespace DirectTorrent.Presentation.Clients.WPFClient.ViewModels
{
    public class HomeViewModel : NotifyPropertyChanged
    {
        public ObservableCollection<HomeMovieItem> ListaFilmova { get; private set; }
        private Visibility _moviesVisibility = Visibility.Collapsed;
        private Visibility _loaderVisibility = Visibility.Visible;

        public HomeViewModel()
        {
            ListaFilmova = new ObservableCollection<HomeMovieItem>();
            BackgroundWorker loader = new BackgroundWorker();
            loader.DoWork += (sender, e) =>
            {
                var lista = MovieRepository.Yify.ListMovies();
                e.Result = lista;
            };
            loader.RunWorkerCompleted += (sender, e) =>
            {
                Dispatcher.CurrentDispatcher.Invoke(() =>
                {
                    foreach (var movie in (IEnumerable<Movie>)e.Result)
                    {
                        ListaFilmova.Add(new HomeMovieItem(movie));
                    }
                });
                LoaderVisibility = Visibility.Collapsed;
                MoviesVisibility = Visibility.Visible;
            };
            loader.RunWorkerAsync();
        }

        public Visibility MoviesVisibility
        {
            get { return this._moviesVisibility; }
            set
            {
                if (this._moviesVisibility != value)
                {
                    this._moviesVisibility = value;
                    OnPropertyChanged("MoviesVisibility");
                }
            }
        }

        public Visibility LoaderVisibility
        {
            get { return this._loaderVisibility; }
            set
            {
                if (this._loaderVisibility != value)
                {
                    this._loaderVisibility = value;
                    OnPropertyChanged("LoaderVisibility");
                }
            }
        }
    }
}