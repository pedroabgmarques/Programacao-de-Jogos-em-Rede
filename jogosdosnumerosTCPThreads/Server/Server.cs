using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using Core.Stores;
using Server.Configs;
using Server.Controllers;

namespace Server
{
    class Server
    {
        /// <summary>
        /// Server controller. 
        /// Create a new instance of the server controller and
        /// call the startserver method
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ServerController serverController = new ServerController();

            serverController.StartServer();
        }
    }
}
