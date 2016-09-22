using Core.Models;
using Core.Models;
using Core.Stores;
using Server.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.Controllers
{

    /// <summary>
    /// Controla o comportamento do servidor
    /// </summary>
    public class ServerController
    {

        public async void StartServer()
        {
            Game game = GameStore.Instance.Game;
            UdpClient udp = new UdpClient(ServerConfigs.Port);
            while (true)
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
                byte[] playerName = udp.Receive(ref endPoint);
                if (game.ListaJogadores == null)
                {
                    game.ListaJogadores = new List<Player>();
                }
                game.ListaJogadores.Add(
                    new Player()
                    {
                        ID = game.ListaJogadores.Count,
                        Nome = Encoding.Unicode.GetChars(playerName).ToString(),
                        IPAddress = endPoint.Address
                    }
                );
                byte[] replyMessage = Encoding.Unicode.GetBytes("Connected");
                await udp.SendAsync(replyMessage, replyMessage.Length, endPoint);
            }
        }

    }
}
