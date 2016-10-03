using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server.Configs
{
    public static class ServerConfig
    {
        /// <summary>
        /// Server configuration settings, we might want to add
        /// the ip address here too, and create a similar 
        /// config file for the client.
        /// </summary>
        public const string IpAddress = "127.0.0.1";

        public const int Port = 7777;

        public const int MaxPlayers = 2;
    }
}
