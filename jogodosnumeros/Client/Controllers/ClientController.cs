using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using Core.Stores;
using Newtonsoft.Json;

namespace Client.Controllers
{
    public class ClientController
    {
        public ClientController()
        {

        }
            
        public void StartClient()
        {
            UdpClient udpClient = new UdpClient();
            while (true)
            {
                Console.WriteLine("Input player name:");
                Player player = new Player
                {
                    PlayerName = Console.ReadLine()
                };
                if (player.PlayerName != null)
                {
                    byte[] msgBytes = Encoding.Unicode.GetBytes(
                            JsonConvert.SerializeObject(
                                new NetworkMessage()
                                {
                                    Player = player
                                })
                            );
                    udpClient.Send(msgBytes, msgBytes.Length, "localhost", 7777);
                    IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 0);
                    byte[] receivedNetworkMessageBytes = udpClient.Receive(ref ipEndPoint);
                    NetworkMessage receivedNetworkMessage =
                            JsonConvert.DeserializeObject<NetworkMessage>(
                                Encoding.Unicode.GetString(receivedNetworkMessageBytes)
                                );
                    if (receivedNetworkMessage.Connected)
                    {
                        Console.WriteLine(receivedNetworkMessage.Message);
                    }
                    else
                    {
                        Console.WriteLine("Not Connected");
                    }

                    // Todo criar enum de estados para cliente e seguir para jogo
                }
            }
        }
    }
}
