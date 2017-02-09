using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace SocketSimpelServerApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            SocketSimpleServer server = new SocketSimpleServer(11000);
            server.Run();
        }
    }

    class SocketSimpleServer
    {
        private IPAddress ip = IPAddress.Parse("127.0.0.1");
        private int port;
        private volatile bool stop = false;

        public SocketSimpleServer(int port)
        {
            this.port = port;
        }

        public void Run()
        {
            System.Console.WriteLine("Simpel server startet p� port:" + port);

            TcpListener listener = new TcpListener(ip, port);
            listener.Start();

            while (!stop)
            {
                System.Console.WriteLine("Simpel server klar");

                // vent p� en klient "kalder op" / "logger p�"
                Socket clientSocket = listener.AcceptSocket();

                System.Console.WriteLine("Der er g�et en i f�lden");

                // ops�t input og output streams
                NetworkStream netStream = new NetworkStream(clientSocket);
                StreamWriter writer = new StreamWriter(netStream);
                StreamReader reader = new StreamReader(netStream);

                // l�s data fra klient
                string clientText = reader.ReadLine();
                Console.WriteLine("Klient siger:" + clientText);

                // skriv data til klient
                writer.WriteLine("Hej Klient");
                writer.Flush();

                // luk forbindelse
                Console.WriteLine("Forbindelse til klient lukkes");
                writer.Close();
                reader.Close();
                netStream.Close();
                clientSocket.Close();
            }
        }
    }

}
