using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

using DirectTorrent.Logic.Models;
using DirectTorrent.Logic.Services;

using DirectTorrent.Presentation.Clients.WPFClient.Models;
using FirstFloor.ModernUI;
using FirstFloor.ModernUI.Presentation;
using GalaSoft.MvvmLight.CommandWpf;

namespace DirectTorrent.Presentation.Clients.WPFClient.ViewModels
{
    public class HomeViewModel : NotifyPropertyChanged
    {
        private Visibility _moviesVisibility = Visibility.Collapsed;
        private Visibility _loaderVisibility = Visibility.Visible;
        //private Quality _selectedQuality = Quality.ALL;
        private Sort _selectedSort = Sort.DateAdded;
        private Order _selectedOrder = Order.Descending;
        //private byte _selectedMinimumRating = 0;
        private string _queryString;

        private uint _currentPage = 1;

        public GalaSoft.MvvmLight.CommandWpf.RelayCommand<ScrollChangedEventArgs> ScrollChangedCommand { get; private set; }
        public GalaSoft.MvvmLight.CommandWpf.RelayCommand TextBoxLostFocus { get; private set; }

        public ObservableCollection<HomeMovieItem> ListaFilmova { get; private set; }

        //public Quality SelectedQuality
        //{
        //    get { return this._selectedQuality; }
        //    set
        //    {
        //        if (this._selectedQuality != value)
        //        {
        //            this._selectedQuality = value;
        //            OnPropertyChanged("SelectedQuality");
        //            LoadMovies(true);
        //        }
        //    }
        //}
        public Sort SelectedSort
        {
            get { return this._selectedSort; }
            set
            {
                if (this._selectedSort != value)
                {
                    this._selectedSort = value;
                    OnPropertyChanged("SelectedSort");
                    LoadMovies(true);
                }
            }
        }
        public Order SelectedOrder
        {
            get { return this._selectedOrder; }
            set
            {
                if (this._selectedOrder != value)
                {
                    this._selectedOrder = value;
                    OnPropertyChanged("SelectedOrder");
                    LoadMovies(true);
                }
            }
        }
        //public byte SelectedMinimumRating
        //{
        //    get { return this._selectedMinimumRating; }
        //    set
        //    {
        //        if (this._selectedMinimumRating != value)
        //        {
        //            this._selectedMinimumRating = value;
        //            OnPropertyChanged("SelectedMinimumRating");
        //            QueryChanged();
        //        }
        //    }
        //}
        public string QueryString
        {
            get { return this._queryString; }
            set
            {
                if (this._queryString != value)
                {
                    this._queryString = value;
                    OnPropertyChanged("QueryString");
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

        public HomeViewModel()
        {
            ListaFilmova = new ObservableCollection<HomeMovieItem>();
            LoadMovies(false);
            this.ScrollChangedCommand = new RelayCommand<ScrollChangedEventArgs>((e) =>
            {
                if (e.VerticalOffset == ((ScrollViewer)e.Source).ScrollableHeight)
                    LoadMovies(false);
            });
            this.TextBoxLostFocus = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(() => LoadMovies(true));
        }

        private void LoadMovies(bool reset)
        {
            if (reset)
            {
                ListaFilmova.Clear();
                _currentPage = 1;
            }

            MoviesVisibility = Visibility.Collapsed;
            LoaderVisibility = Visibility.Visible;
            BackgroundWorker loader = new BackgroundWorker();
            loader.DoWork += (sender, e) =>
            {
                var lista = MovieRepository.Yify.ListMovies(page: _currentPage/*, quality: _selectedQuality*/, sortBy: _selectedSort, orderBy: _selectedOrder, queryTerm: _queryString);
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
                _currentPage++;
                LoaderVisibility = Visibility.Collapsed;
                MoviesVisibility = Visibility.Visible;
            };
            loader.RunWorkerAsync();
        }
    }
}