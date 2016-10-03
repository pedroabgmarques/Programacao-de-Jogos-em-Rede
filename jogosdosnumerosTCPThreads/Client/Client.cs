using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Controllers;

namespace Client
{
    class Client
    {
        /// <summary>
        /// This class is responsible for initiating the client app
        /// All we do is create a new clientController instance
        /// and call the startclient method.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ClientController clientController = new ClientController();
            clientController.StartClient();
        }
    }
}
