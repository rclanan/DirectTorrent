using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;
using System.Net;

namespace DirectTorrent.Logic.Services
{
    public static class NodeServerManager
    {
        private static Process server;
        /// <summary>
        /// Starts the node streaming server.
        /// </summary>
        /// <param name="magnetUri">The magnetUri of the torrent to be streamed.</param>
        public static void StartServer(string magnetUri)
        {
            // Start the server and pass the torrent uri.
            ProcessStartInfo info = new ProcessStartInfo() { FileName = "DirectTorrent.Logic.NodeServer.exe", Arguments = magnetUri };
            server = Process.Start(info);
        }

        /// <summary>
        /// Closes the node server.
        /// </summary>
        public static void CloseServer()
        {
            // Kills the server
            if (server != null)
                server.Kill();
        }
    }
}