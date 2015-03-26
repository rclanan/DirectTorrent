using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

using DirectTorrent.Logic.Models;
using DirectTorrent.Logic.Services;
using FirstFloor.ModernUI.Presentation;
using GalaSoft.MvvmLight;

namespace DirectTorrent.Presentation.Clients.WPFClient.ViewModels
{
    public class MovieDetailsViewModel : ViewModelBase
    {
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
        //private health

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

        public MovieDetailsViewModel(int movieId)
        {
            LoadMovie(movieId);
        }

        public void SetNewMovie(int movieId)
        {
            LoadMovie(movieId);
        }

        private void LoadMovie(int movId)
        {
            this.LoaderVisibility = Visibility.Visible;
            this.MovieVisibility = Visibility.Collapsed;

            BackgroundWorker loader = new BackgroundWorker();
            loader.DoWork += (sender, e) =>
            {
                e.Result = MovieRepository.Yify.GetMovieDetails(movId);
            };
            loader.RunWorkerCompleted += (sender, e) =>
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
                CurrentId = movId;
                this.LoaderVisibility = Visibility.Collapsed;
                this.MovieVisibility = Visibility.Visible;
            };
            loader.RunWorkerAsync();
        }
    }
}
