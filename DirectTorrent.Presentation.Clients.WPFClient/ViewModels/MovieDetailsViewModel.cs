using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

using DirectTorrent.Logic.Models;
using DirectTorrent.Logic.Services;
using DirectTorrent.Presentation.Clients.WPFClient.Views;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MovieDetails = DirectTorrent.Logic.Models.MovieDetails;

namespace DirectTorrent.Presentation.Clients.WPFClient.ViewModels
{

    public class MovieDetailsViewModel : ViewModelBase
    {
        public enum TorrentHealth
        {
            Bad,
            Good,
            Excellent
        };

        public enum ReproductionMethod
        {
            WMP,
            Browser,
            Local
        };

        public static int CurrentId { get; private set; }

        private BitmapImage _movieImage;
        private string _movieTitle;
        private string _movieDescription;
        private int _movieYear;
        private int _movieDuration;
        private string _movieGenre;
        private Uri _imdbLink;
        private double _movieRating;
        private Visibility _loaderVisibility = Visibility.Visible;
        private Visibility _movieVisibility = Visibility.Collapsed;
        private TorrentHealth _movieHealth;
        private bool _hasFhd = false;
        private Quality _selectedQuality = Quality.HD;
        private Torrent[] torrents = new Torrent[2];

        public GalaSoft.MvvmLight.CommandWpf.RelayCommand PlayButtonClicked { get; private set; }

        public string MovieTitle
        {
            get { return this._movieTitle; }
            private set
            {
                if (this._movieTitle != value)
                {
                    this._movieTitle = value;
                    RaisePropertyChanged("MovieTitle");
                }
            }
        }
        public string MovieDescription
        {
            get { return this._movieDescription; }
            private set
            {
                if (this._movieDescription != value)
                {
                    this._movieDescription = value;
                    RaisePropertyChanged("MovieDescription");
                }
            }
        }
        public int MovieYear
        {
            get { return this._movieYear; }
            private set
            {
                if (this._movieYear != value)
                {
                    this._movieYear = value;
                    RaisePropertyChanged("MovieYear");
                }
            }
        }
        public int MovieDuration
        {
            get { return this._movieDuration; }
            private set
            {
                if (this._movieDuration != value)
                {
                    this._movieDuration = value;
                    RaisePropertyChanged("MovieDuration");
                }
            }
        }
        public double MovieRating
        {
            get { return this._movieRating; }
            private set
            {
                if (this._movieRating != value)
                {
                    this._movieRating = value;
                    RaisePropertyChanged("MovieRating");
                }
            }
        }
        public string MovieGenre
        {
            get { return this._movieGenre; }
            private set
            {
                if (this._movieGenre != value)
                {
                    this._movieGenre = value;
                    RaisePropertyChanged("MovieGenre");
                }
            }
        }
        public BitmapImage MovieImage
        {
            get { return this._movieImage; }
            private set
            {
                if (this._movieImage != value)
                {
                    this._movieImage = value;
                    RaisePropertyChanged("MovieImage");
                }
            }
        }
        public Uri ImdbLink
        {
            get { return this._imdbLink; }
            private set
            {
                if (this._imdbLink != value)
                {
                    this._imdbLink = value;
                    RaisePropertyChanged("ImdbLink");
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
        public Visibility MovieVisibility
        {
            get { return this._movieVisibility; }
            private set
            {
                if (this._movieVisibility != value)
                {
                    this._movieVisibility = value;
                    RaisePropertyChanged("MovieVisibility");
                }
            }
        }
        public TorrentHealth MovieHealth
        {
            get { return this._movieHealth; }
            private set
            {
                if (this._movieHealth != value)
                {
                    this._movieHealth = value;
                    RaisePropertyChanged("MovieHealth");
                }
            }
        }
        public void SetNewMovie(int movieId)
        {
            LoadMovie(movieId);
        }
        public bool HasFhd
        {
            get { return this._hasFhd; }
            private set
            {
                if (this._hasFhd != value)
                {
                    this._hasFhd = value;
                    RaisePropertyChanged("HasFhd");
                }
            }
        }
        public Quality SelectedQuality
        {
            get { return this._selectedQuality; }
            private set
            {
                if (this._selectedQuality != value)
                {
                    if (value == Quality.HD)
                        SetTorrentHealth(0);
                    else if (value == Quality.FHD)
                        SetTorrentHealth(1);
                    this._selectedQuality = value;
                    RaisePropertyChanged("SelectedQuality");
                }
            }
        }

        public MovieDetailsViewModel(int movieId)
        {
            this.PlayButtonClicked = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(() =>
            {
                string magnetUri = String.Empty;
                switch (SelectedQuality)
                {
                    case Quality.HD:
                        magnetUri = MovieRepository.GetTorrentMagnetUri(this.torrents[0].Hash, this.MovieTitle);
                        break;
                    case Quality.FHD:
                        magnetUri = MovieRepository.GetTorrentMagnetUri(this.torrents[1].Hash, this.MovieTitle);
                        break;
                }
                var wind = new MovieVideo(magnetUri);
                wind.ShowDialog();

            });
            LoadMovie(movieId);
        }

        private void LoadMovie(int movId)
        {
            this.LoaderVisibility = Visibility.Visible;
            this.MovieVisibility = Visibility.Collapsed;

            BackgroundWorker loader = new BackgroundWorker();
            loader.DoWork += (sender, e) =>
            {
                try
                {
                    e.Result = MovieRepository.Yify.GetMovieDetails(movId);
                }
                catch (Exception)
                {
                    e.Cancel = true;
                }
            };
            loader.RunWorkerCompleted += (sender, e) =>
            {
                if (!e.Cancelled)
                {
                    var movie = (MovieDetails)e.Result;
                    this.MovieTitle = movie.Title;
                    this.MovieDescription = movie.DescriptionFull;
                    this.MovieYear = movie.Year;
                    this.MovieDuration = movie.Runtime;
                    this.MovieRating = movie.Rating;
                    StringBuilder genres = new StringBuilder();
                    movie.Genres.ForEach(x => genres.Append(x + "/"));
                    var genre = genres.ToString();
                    genre = genre.Remove(genre.Length - 1);
                    this.MovieGenre = genre;
                    this.MovieImage = new BitmapImage(new Uri(movie.Images.LargeCoverImage, UriKind.Absolute));
                    this.ImdbLink = new Uri("http://www.imdb.com/title/" + movie.ImdbCode + "/", UriKind.Absolute);
                    movie.Torrents.CopyTo(this.torrents, 0);
                    if (movie.Torrents.Count > 1)
                    {
                        this.HasFhd = true;
                        this.SelectedQuality = Quality.FHD;
                        SetTorrentHealth(1);
                    }
                    else
                        SetTorrentHealth(0);
                    CurrentId = movId;
                    this.LoaderVisibility = Visibility.Collapsed;
                    this.MovieVisibility = Visibility.Visible;
                }
            };
            loader.RunWorkerAsync();
        }

        private void SetTorrentHealth(int torrentId)
        {
            double ratio = 0;
            try
            {
                ratio = this.torrents[torrentId].Seeds / this.torrents[torrentId].Peers;
            }
            catch
            {

            }
            if (ratio < 1)
                this.MovieHealth = TorrentHealth.Bad;
            else if (ratio >= 1 && ratio <= 1.5)
                this.MovieHealth = TorrentHealth.Good;
            else if (ratio > 1.5)
                this.MovieHealth = TorrentHealth.Excellent;
        }
    }
}
