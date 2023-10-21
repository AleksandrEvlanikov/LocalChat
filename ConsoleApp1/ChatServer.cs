using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp
{
    public class ChatServer
    {
        static public TcpListener server;
        static public List<TcpClient> clients = new List<TcpClient>();
        static public List<StreamWriter> clientWriters = new List<StreamWriter>();



        public static void HandleClient(TcpClient client, StreamReader reader)
        {
            string clientName = reader.ReadLine();
            Console.WriteLine($"{clientName} Присоединился к чату.");
            BroadcastMessage($"{clientName} Присоединился к чату. Connect ty server.");

            string message;
            try
            {
                while (true)
                {
                    message = reader.ReadLine();
                    Console.WriteLine($"{message}");
                    BroadcastMessage($"{message}");
                }
            }
            catch( Exception ex ) 
            {
                Console.WriteLine($"{clientName} покинул чат.");
                clients.Remove(client);
                clientWriters.Remove(clientWriters[clients.IndexOf(client)]);
                BroadcastMessage($"{clientName} покинул чат.");
                client.Close();
            }
        }




        public static void BroadcastMessage(String message)
        {
            foreach (var client in clients)
            {
                StreamWriter streamWriter = new StreamWriter(client.GetStream());
                streamWriter.WriteLine(message);
                streamWriter.Flush();
            }


        }

    }
}