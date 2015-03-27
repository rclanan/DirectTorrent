using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Reflection;

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
            string server = string.Empty;
            try
            {
                var _assembly = Assembly.GetExecutingAssembly();
                using (var sr = new StreamReader(_assembly.GetManifestResourceStream("DirectTorrent.Logic.NodeServer.server.js")))
                {
                    server = sr.ReadToEnd();
                }
                //File.WriteAllText("test", _textStreamReader.ReadToEnd());
                //RADI!
            }
            catch
            {
                throw new Exception("Error accessing server resource!");
            }

            server = server.Replace("MATIJACUPIC", args[0]);
            //File.WriteAllText("test.txt",server);
            var a = Start(server).Result;
            //string server = File.ReadAllText("test2.js");
            //server = server.Replace("MATIJACUPIC", args[0]);
            // Runs the start method synchronously 
            //var a = Start(server).Result;
        }

        private static async Task<object> Start(string server)
        {

            // Creates a node.js server that serves the movie in the torrent
            var createHttpServer = Edge.Func(server);

            return await createHttpServer(8080);
        }
    }
}
