using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp
{
    public class ChatClient 
    {
        public static string nickName;


        public static void ReceiveMessages(object streamReader)
        {
            StreamReader streamReader1 = (StreamReader )streamReader;
            while (true)
            {
                try
                {
                    string massage = streamReader1.ReadLine();
                    Console.WriteLine(massage);
                }
                catch
                {
                    Console.WriteLine("Произошла ошибка или сервер закрыл соединение.");
                    break;
                }
            }
        }

        public static void WriteMessages(object streamReader) 
        {
            StreamWriter streamWriter1 = (StreamWriter )streamReader;

            while(true)
            {
                try
                {
                    string message = Console.ReadLine();
                    streamWriter1.WriteLine($"{nickName}: {message}\n");
                    streamWriter1.Flush();
                }
                catch
                {
                    Console.WriteLine($"Произошла ошибка или сервер закрыл соединение.");
                }
            }
        }

    }
}
