using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Client
    {
        static void Main(string[] args)
        {
            try
            {
                

                while (true)
                {
                    TcpClient tcpClient = new TcpClient();
                    tcpClient.Connect("127.0.0.1", 7777);

                    String messageString = Console.ReadLine();
                    Stream stream = tcpClient.GetStream();

                    ASCIIEncoding asen = new ASCIIEncoding();
                    byte[] messageBytes = asen.GetBytes(messageString);

                    stream.Write(messageBytes, 0, messageBytes.Length);

                    byte[] responseBytes = new byte[100];
                    int responseBytesBuffer = stream.Read(responseBytes, 0, 100);

                    for (int i = 0; i < responseBytesBuffer; i++)
                    {
                        Console.Write(Convert.ToChar(responseBytes[i]));
                    }

                    tcpClient.Close();
                }
                

                
                
            }

            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.ReadLine();
            }
        }
    }
}
