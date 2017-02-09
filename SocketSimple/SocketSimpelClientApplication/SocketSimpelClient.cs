using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace SocketSimpelClientApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            SocketSimpleClient client = new SocketSimpleClient("127.0.0.1", 11000);
            client.Run();
        }
    }

    class SocketSimpleClient
    {
        private string servername;
        private int port;

        public SocketSimpleClient(string servername, int port)
        {
            this.servername = servername;
            this.port = port;
        }
        public void Run()
        {
            System.Console.WriteLine("Simpel client startet på " + servername + " port:" + port);

            //// Instantiér socket - forbinder socket til server
            TcpClient server = new TcpClient(servername, port);

            // opsæt input og output streams
            NetworkStream stream = server.GetStream();
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);

            // send besked til server
            writer.WriteLine("Hej Server"); // Skriver tekst til serveren
            writer.Flush(); // Tømmer tcp-bufferen

            // læs svar fra server
            string serverData = reader.ReadLine(); // Læser besked fra server
            Console.WriteLine(serverData); // Skriver besked til skærmen

            Console.WriteLine("Forbindelsen til serveren lukkes ned ...\n");
            writer.Close();
            reader.Close();
            stream.Close();
            server.Close();

            Console.ReadLine(); // vent på retur
        }
    }
}
