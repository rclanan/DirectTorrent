using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DirectTorrent.Presentation.Clients.WPFClient
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Stopwatch timer = System.Diagnostics.Stopwatch.StartNew();
            var temp = DirectTorrent.Logic.Services.MovieRepository.Yify.ListUpcomingMovies();
            Debug.WriteLine("Total execution time: " + timer.Elapsed);
            MessageBox.Show(temp[0].Title);
        }
    }
}
