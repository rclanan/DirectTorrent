using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using System.IO;
using System.Resources;
using FirstFloor.ModernUI.Presentation;

namespace DirectTorrent.Presentation.Clients.WPFClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            switch (WPFClient.Properties.Settings.Default["Theme"].ToString())
            {
                case "Light":
                    AppearanceManager.Current.ThemeSource = AppearanceManager.LightThemeSource;
                    break;
                case "Dark":
                    AppearanceManager.Current.ThemeSource = AppearanceManager.DarkThemeSource;
                    break;
            }
        }
    }
}
