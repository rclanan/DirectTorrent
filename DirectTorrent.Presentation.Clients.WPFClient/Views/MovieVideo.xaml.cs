using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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


using DirectTorrent.Logic.Services;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Channels;
using AxWMPLib;

namespace DirectTorrent.Presentation.Clients.WPFClient.Views
{
    /// <summary>
    /// Interaction logic for MovieVideo.xaml
    /// </summary>
    public partial class MovieVideo : ModernWindow
    {

        public MovieVideo(string magnetUri)
        {
            InitializeComponent();
            //AxWMPLib.AxWindowsMediaPlayer player = new AxWindowsMediaPlayer();
            //Host.Child = player;
            if (magnetUri != string.Empty)
            {
                NodeServerManager.StartServer(magnetUri);
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += (sender, e) =>
                {
                    while (true)
                    {
                        if (File.Exists("hash.txt"))
                            break;
                    }
                };
                worker.RunWorkerCompleted += (sender, e) =>
                {
                    //this.Player.closedCaption.SAMIFileName=
                    this.Player.URL = "http://localhost:1337";
                };
                worker.RunWorkerAsync();

                //this.Player.URL = "http://localhost:1337";
                //axWmp.URL = "http://localhost:1337";
            }
        }

        private void ModernWindow_Closing(object sender, CancelEventArgs e)
        {
            NodeServerManager.CloseServer();
        }
    }
}
