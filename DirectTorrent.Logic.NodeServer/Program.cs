using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using EdgeJs;

namespace DirectTorrent.Logic.NodeServer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var a = Start().Result;

        }

        private static async Task<object> Start()
        {
            var createHttpServer = Edge.Func(File.ReadAllText("test.js"));

            return await createHttpServer(8080);
        }
    }
}
