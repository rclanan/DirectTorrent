using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Threading;

using DirectTorrent.Logic.Models;
using DirectTorrent.Logic.Services;

using DirectTorrent.Presentation.Clients.WPFClient.Models;
using FirstFloor.ModernUI;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using GalaSoft.MvvmLight.CommandWpf;

namespace DirectTorrent.Presentation.Clients.WPFClient.ViewModels
{
    public class HomeViewModel : GalaSoft.MvvmLight.ViewModelBase
    {
        //private IModernNavigationService _modernNavigationService;
        private Visibility _moviesVisibility = Visibility.Collapsed;
        private Visibility _loaderVisibility = Visibility.Visible;
        //private Quality _selectedQuality = Quality.ALL;
        private Sort _selectedSort = Sort.DateAdded;
        private Order _selectedOrder = Order.Descending;
        //private byte _selectedMinimumRating = 0;
        private string _queryString;
        private bool _isLoading = false;

        private uint _currentPage = 1;

        public GalaSoft.MvvmLight.CommandWpf.RelayCommand<ScrollChangedEventArgs> ScrollChangedCommand { get; private set; }
        public GalaSoft.MvvmLight.CommandWpf.RelayCommand TextBoxLostFocus { get; private set; }
        public GalaSoft.MvvmLight.CommandWpf.RelayCommand<int> MovieClicked { get; private set; }

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
                    RaisePropertyChanged("SelectedSort");
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
                    RaisePropertyChanged("SelectedOrder");
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
                    RaisePropertyChanged("QueryString");
                }
            }
        }
        public Visibility LoaderVisibility
        {
            get { return this._loaderVisibility; }
            private set
            {
                if (this._loaderVisibility != value)
                {
                    this._loaderVisibility = value;
                    RaisePropertyChanged("LoaderVisibility");
                }
            }
        }
        public Visibility MoviesVisibility
        {
            get { return this._moviesVisibility; }
            private set
            {
                if (this._moviesVisibility != value)
                {
                    this._moviesVisibility = value;
                    RaisePropertyChanged("MoviesVisibility");
                }
            }
        }
        public bool IsLoading
        {
            get { return this._isLoading; }
            private set
            {
                if (this._isLoading != value)
                {
                    this._isLoading = value;
                    RaisePropertyChanged("IsLoading");
                }
            }
        }

        public HomeViewModel(/*IModernNavigationService modernNavigationService*/)
        {
            //try
            //{
            //    _modernNavigationService = modernNavigationService;
            //}
            //catch (Exception e)
            //{

            //    throw new Exception(e.Message);
            //}

            ListaFilmova = new ObservableCollection<HomeMovieItem>();
            LoadMovies(false);
            this.ScrollChangedCommand = new RelayCommand<ScrollChangedEventArgs>((e) =>
            {
                if (e.VerticalOffset == ((ScrollViewer)e.Source).ScrollableHeight)
                    LoadMovies(false);
            });
            this.TextBoxLostFocus = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(() => LoadMovies(true));
            this.MovieClicked = new RelayCommand<int>(x =>
            {
                Data.MovieId = x;
                (Application.Current.MainWindow as MainWindow).ContentSource = new Uri("/Views/MovieDetails.xaml", UriKind.Relative);
                //_modernNavigationService.NavigateTo(ViewModelLocator.MovieDetailsPageKey, x);
            });
        }

        private void LoadMovies(bool reset)
        {
            if (reset)
            {
                MoviesVisibility = Visibility.Collapsed;
                LoaderVisibility = Visibility.Visible;
                ListaFilmova.Clear();
                _currentPage = 1;
            }
            else
                IsLoading = true;
            BackgroundWorker loader = new BackgroundWorker();
            loader.DoWork += (sender, e) =>
            {
                try
                {
                    var lista = MovieRepository.Yify.ListMovies(page: _currentPage/*, quality: _selectedQuality*/, sortBy: _selectedSort, orderBy: _selectedOrder, queryTerm: _queryString);
                    e.Result = lista;
                }
                catch (WebException)
                {
                    e.Cancel = true; 
                }
            };
            loader.RunWorkerCompleted += (sender, e) =>
            {
                if (!e.Cancelled)
                {
                    Dispatcher.CurrentDispatcher.Invoke(() =>
                    {
                        foreach (var movie in (IEnumerable<Movie>) e.Result)
                        {
                            ListaFilmova.Add(new HomeMovieItem(movie));
                        }
                    });
                    _currentPage++;
                    IsLoading = false;
                    LoaderVisibility = Visibility.Collapsed;
                    MoviesVisibility = Visibility.Visible;
                }
                else
                {
                    LoaderVisibility = Visibility.Collapsed;
                    ModernDialog.ShowMessage("No internet connection!" + Environment.NewLine + "Please restart the application with internet access.", "No internet access!", MessageBoxButton.OK);
                }
            };
            loader.RunWorkerAsync();
        }
    }
}