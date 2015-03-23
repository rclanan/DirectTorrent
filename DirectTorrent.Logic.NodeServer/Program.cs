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
        static void Main(string[] args)
        {
            string server = File.ReadAllText("test2.js");
            server = server.Replace("MATIJACUPIC", args[0]);
            // Runs the start method synchronously 
            var a = Start(server).Result;
        }

        private static async Task<object> Start(string server)
        {

            // Creates a node.js server that serves the movie in the torrent
            var createHttpServer = Edge.Func(server);

            return await createHttpServer(8080);
        }
    }
}
