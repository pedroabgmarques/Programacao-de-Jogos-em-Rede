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
                while (true)
                {
                    Socket socket = tcpListener.AcceptSocket();
                
                    byte[] messageByte = new byte[100];
                    int messageByteBuffer = socket.Receive(messageByte);

                    for (int i = 0; i < messageByteBuffer; i++)
                    {
                        Console.Write(Convert.ToChar(messageByte[i]));
                    }
                    Console.WriteLine("\n");
                    ASCIIEncoding asciiEncoding = new ASCIIEncoding();
                    socket.Send(asciiEncoding.GetBytes("Hello Client."));

                    socket.Close();
                }
                
                
                tcpListener.Stop();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.ReadLine();
            }
        }
    }
}
