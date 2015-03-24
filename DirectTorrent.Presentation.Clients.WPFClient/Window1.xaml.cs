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

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            var listaFilmova = DirectTorrent.Logic.Services.MovieRepository.Yify.GetDummyData();
            lbl1.Content = listaFilmova;
        }

        private void btnStartNode_Click(object sender, RoutedEventArgs e)
        {
            var listaFilmova = DirectTorrent.Logic.Services.MovieRepository.Yify.ListMovies();
            var torrent = MovieRepository.GetTorrentMagnetUri(listaFilmova[0].Torrents[0].Hash,
                listaFilmova[0].TitleLong);
            //File.WriteAllText("fajl.txt", torrent);
            NodeServerManager.StartServer(torrent);
        }

        private void btnStopNode_Click(object sender, RoutedEventArgs e)
        {
            NodeServerManager.CloseServer();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            NodeServerManager.CloseServer();
        }
    }
}
