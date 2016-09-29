using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Common;

namespace Client
{
    class Client
    {
        static void Main(string[] args)
        {
            try
            {

                Console.WriteLine("Client connected to server.");
                
                while (true)
                {
                    TcpClient tcpClient = new TcpClient();
                    tcpClient.Connect("127.0.0.1", 7777);
                    
                    Console.WriteLine("\n\nInsert message to send to server:");

                    //Ler a mensagem e convertê-la para bytes
                    String messageString = Console.ReadLine();
                    byte[] messageBytes = Encoding.Unicode.GetBytes(
                        JsonConvert.SerializeObject(
                            new Mensagem()
                            {
                                Message = messageString
                            })
                        );

                    //Enviar a mensagem para o servidor
                    Stream stream = tcpClient.GetStream();
                    stream.Write(messageBytes, 0, messageBytes.Length);

                    //Ler resposta do servidor
                    byte[] responseBytes = new byte[10000];
                    int responseBytesBuffer = stream.Read(responseBytes, 0, 10000);

                    if (responseBytesBuffer > 0)
                    {
                        Console.WriteLine("\nAnswer from server received:");
                    }

                    string message = Encoding.Unicode.GetString(responseBytes);
                    Mensagem receivedNetworkMessage =
                        JsonConvert.DeserializeObject<Mensagem>(
                            Encoding.Unicode.GetString(responseBytes).ToString()
                            );

                    //Escrever a mensagem recebida
                    Console.WriteLine(receivedNetworkMessage.Message);

                    if (!ProcessMessage(receivedNetworkMessage, stream))
                    {
                        break;
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

        static private bool ProcessMessage(Mensagem message, Stream stream)
        {
            string m = message.Message;

            if (m == "bye" || m == "exit" || m == "quit")
            {
                return false;
            }

            return true;
        }
    }
}
