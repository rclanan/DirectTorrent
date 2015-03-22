using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using DirectTorrent.Logic.Services;
using System.Threading.Tasks;
using System.Xml;

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

        private Process server;
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ProcessStartInfo info = new ProcessStartInfo() { FileName = "DirectTorrent.Logic.NodeServer.exe" };
            server = Process.Start(info);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            new WebClient().DownloadData("http://localhost:1337/shutdown.html");
        }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            var listaFilmova = DirectTorrent.Logic.Services.MovieRepository.Yify.GetMovieDetails(1124);
            lbl1.Content = listaFilmova.Actors[0].Name;
        }
    }
}
