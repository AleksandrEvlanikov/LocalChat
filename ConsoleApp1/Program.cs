using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            int port = 55555;
            string serverIpAddress = "127.0.0.1";

            ChatServer.server = new TcpListener(IPAddress.Any, port);
            ChatServer.server.Start();
            Console.WriteLine($"Сервер слушает порт = {port}");
            Console.WriteLine($"Сервер слушает IP = {serverIpAddress}");

            while (true)
            {
                TcpClient client = ChatServer.server.AcceptTcpClient();
                ChatServer.clients.Add(client);

                NetworkStream stream = client.GetStream();
                StreamReader reader = new StreamReader(stream, Encoding.ASCII);
                StreamWriter writer = new StreamWriter(stream, Encoding.ASCII) { AutoFlush = true };
                ChatServer.clientWriters.Add(writer);

                Thread clienntThread = new Thread(() => ChatServer.HandleClient(client, reader));
                clienntThread.Start();
            }
        }
    }
}
