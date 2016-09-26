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
            //try
            //{
                
                
                Console.WriteLine("Client connected to server.");
                
                while (true)
                {
                    TcpClient tcpClient = new TcpClient();
                    tcpClient.Connect("127.0.0.1", 7777);
                    
                    Console.WriteLine("\n\nInsert message to send to server:");

                    //Ler a mensagem e convertê-la para bytes
                    String messageString = Console.ReadLine();
                    ASCIIEncoding asen = new ASCIIEncoding();
                    byte[] messageBytes = asen.GetBytes(messageString);

                    //Enviar a mensagem para o servidor
                    Stream stream = tcpClient.GetStream();
                    stream.Write(messageBytes, 0, messageBytes.Length);

                    //Ler resposta do servidor
                    byte[] responseBytes = new byte[100];
                    int responseBytesBuffer = stream.Read(responseBytes, 0, 100);

                    if (responseBytesBuffer > 0)
                    {
                        Console.WriteLine("\nAnswer from server received:");
                    }

                    //Escrever mensagem recebida do servidor
                    for (int i = 0; i < responseBytesBuffer; i++)
                    {
                        Console.Write(Convert.ToChar(responseBytes[i]));
                    }

                    tcpClient.Close();

                    
                }

                //
                
                
            }

            //catch (Exception e)
            //{
            //    Console.WriteLine(e.StackTrace);
            //    Console.ReadLine();
            //}
        //}
    }
}
