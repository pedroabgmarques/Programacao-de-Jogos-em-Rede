using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpClient clientSocket = new TcpClient();
            clientSocket.Connect("127.0.0.1", 8888);
            Console.WriteLine("Client connected.");

            NetworkStream serverStream = clientSocket.GetStream();
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes("Teste de ligacao!$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();

            clientSocket.GetStream().Close();
            clientSocket.Close();

            Console.WriteLine("Mensagem enviada.");
        }
    }
}
