using System.Net.Sockets;
using System.Text;

namespace ClientApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Введите ваш никнейм. ");
            ChatClient.nickName = Console.ReadLine();

            string serverIp = "127.0.0.1";
            int serverPort = 55555;


            TcpClient client = new TcpClient();
            client.Connect(serverIp, serverPort);
            Console.WriteLine($"Подключился к серверу = {serverIp}");

            NetworkStream stream = client.GetStream();
            StreamReader streamReader = new StreamReader(stream, Encoding.ASCII);
            StreamWriter streamWriter = new StreamWriter(stream, Encoding.ASCII);

            streamWriter.WriteLine(ChatClient.nickName);
            streamWriter.Flush();

            Thread receiveThread = new Thread(ChatClient.ReceiveMessages);
            Thread writeThread = new Thread(ChatClient.WriteMessages);

            receiveThread.Start(streamReader);
            writeThread.Start(streamWriter);
        }
    }
}