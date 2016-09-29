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

        static int nPensado;
        static bool jogoIniciado;
        static Random rnd;

        static void Main(string[] args)
        {

            nPensado = 0;
            jogoIniciado = false;
            rnd = new Random();

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

                    if (!ProcessMessage(receivedNetworkMessage, socket))
                    {
                        break;
                    }

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

        static private bool ProcessMessage(Mensagem message, Socket socket){

            string m = message.Message;

            if (m == "bye" || m == "exit" || m == "quit")
            {
                SendMessage("bye", socket);
                return false;
            }

            if (!jogoIniciado)
            {
                if (m == "hello")
                {
                    SendMessage("Hello client!", socket);
                }else
                if (m == "start" || m == "begin")
                {
                    SendMessage("Game started! Im thinking about a number from 1 to 100.. try to guess it!", socket);
                    Console.WriteLine("\nGame started.");

                    jogoIniciado = true;
                    nPensado = rnd.Next(0, 100);
                }
                else
                {
                    SendMessage("Command not recognize. Available commands: hello, bye, start.", socket);
                }
            }
            else
            {
                //Estamos a jogar, só aceitamos inteiros como input
                try
                {
                    int nTentativa = Int32.Parse(m);

                    //Verificar se está dentro dos limites
                    if (nTentativa >= 0 && nTentativa <= 100)
                    {
                        if (nTentativa == nPensado)
                        {
                            SendMessage("You won the game! Congrats. type start to begin a new game.", socket);
                            nPensado = 0;
                            jogoIniciado = false;
                        }
                        else if (nTentativa < nPensado)
                        {
                            SendMessage("I'm thinking on a bigger number..", socket);
                        }
                        else if (nTentativa > nPensado)
                        {
                            SendMessage("I'm thinking on a smaller number..", socket);
                        }
                    }
                    else
                    {
                        //Fora dos limites
                        SendMessage("The number you must guess is between 0 and 100.", socket);
                    }
                }
                catch(Exception e)
                {
                    SendMessage("Invalid input. Only integers.", socket);
                    return true;
                }
            }
            
            return true;
                    
        }

        static private void SendMessage(string message, Socket socket)
        {
            byte[] messageBytes = Encoding.Unicode.GetBytes(
                            JsonConvert.SerializeObject(
                                new Mensagem()
                                {
                                    Message = message
                                })
                            );
            socket.Send(messageBytes);
        }
    }
}
