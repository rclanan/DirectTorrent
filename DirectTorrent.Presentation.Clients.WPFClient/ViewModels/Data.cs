using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DirectTorrent.Presentation.Clients.WPFClient.ViewModels
{
    public static class Data
    {
        private static int _movieId = -1;

        public static int MovieId
        {
            get
            {
                return _movieId;
            }
            set
            {
                if (_movieId != value)
                    _movieId = value;
            }
        }
    }
}
