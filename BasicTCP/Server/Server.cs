using Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Server
    {
        static void Main(string[] args)
        {
            try
            {
                IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
                TcpListener tcpListener = new TcpListener(ipAddress, 7777);
                tcpListener.Start();
                Console.WriteLine("Server listening.");

                while (true)
                {
                    Socket socket = tcpListener.AcceptSocket();

                    IPEndPoint remoteIpEndPoint = socket.RemoteEndPoint as IPEndPoint;

                    byte[] messageByte = new byte[100];
                    int messageByteBuffer = socket.Receive(messageByte);

                    if (remoteIpEndPoint != null)
                    {
                        Console.WriteLine("\nMensagem recebida!");
                        Console.WriteLine("De: " + remoteIpEndPoint.Address + ":" + remoteIpEndPoint.Port);

                    }

                    //Converter a mensagem recebida em bytes para um objeto Mensagem
                    string message = Encoding.Unicode.GetString(messageByte);
                    Mensagem receivedNetworkMessage =
                        JsonConvert.DeserializeObject<Mensagem>(
                            Encoding.Unicode.GetString(messageByte).ToString()
                            );

                    //Escrever a mensagem recebida
                    Console.WriteLine(receivedNetworkMessage.Message);

                    //Enviar mensagem
                    byte[] messageBytes = Encoding.Unicode.GetBytes(
                        JsonConvert.SerializeObject(
                            new Mensagem()
                            {
                                Message = "Hello client!"
                            })
                        );
                    socket.Send(messageBytes);

                    socket.Close();

                }

                //tcpListener.Stop();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }
    }
}
