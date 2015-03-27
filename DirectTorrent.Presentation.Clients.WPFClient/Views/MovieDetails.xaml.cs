using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using DirectTorrent.Presentation.Clients.WPFClient.ViewModels;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Controls;

namespace DirectTorrent.Presentation.Clients.WPFClient.Views
{
    /// <summary>
    /// Interaction logic for MovieDetails.xaml
    /// </summary>
    public partial class MovieDetails : UserControl, IContent
    {
        public MovieDetails()
        {
            InitializeComponent();
        }

        private bool first = true;
        void IContent.OnFragmentNavigation(FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs e)
        {
        }

        void IContent.OnNavigatedFrom(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
        }

        void IContent.OnNavigatedTo(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
            if (Data.MovieId != -1)
            {
                if (first)
                {
                    this.DataContext = new MovieDetailsViewModel(Data.MovieId);
                    first = false;
                }
                else if (Data.MovieId != MovieDetailsViewModel.CurrentId)
                    ((MovieDetailsViewModel)this.DataContext).SetNewMovie(Data.MovieId);
            }
            else
            {
                ((ModernWindow)Application.Current.MainWindow).ContentSource = new Uri("/Views/Home.xaml", UriKind.Relative);
                ModernDialog.ShowMessage("Please select a movie first!", "No movie selected!", MessageBoxButton.OK);
            }
        }

        void IContent.OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
        {
        }
    }

    public class QualityToRadioButtonConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value.Equals(parameter);
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //throw new NotImplementedException();
            return value.Equals(true) ? parameter : Binding.DoNothing;
        }
    }
}
