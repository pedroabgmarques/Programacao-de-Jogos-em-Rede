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
                
                    byte[] messageByte = new byte[100];
                    int messageByteBuffer = socket.Receive(messageByte);

                    IPEndPoint remoteIpEndPoint = socket.RemoteEndPoint as IPEndPoint;

                    if (remoteIpEndPoint != null)
                    {
                        Console.WriteLine("\nMensagem recebida!");
                        Console.WriteLine("De: " + remoteIpEndPoint.Address + ":" + remoteIpEndPoint.Port);
                        
                    }

                    char[] msgByteArray = new char[messageByteBuffer];
                    //Escrever a mensagem recebida
                    for (int i = 0; i < messageByteBuffer; i++)
                    {
                        msgByteArray[i] = Convert.ToChar(messageByte[i]);
                    }
                    string mensagem = new string(msgByteArray);

                    Console.WriteLine(mensagem + "\n");

                    //Enviar mensagem
                    ASCIIEncoding asciiEncoding = new ASCIIEncoding();
                    socket.Send(asciiEncoding.GetBytes("Hello Client."));

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
