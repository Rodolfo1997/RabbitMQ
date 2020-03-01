using System;

namespace RabbitMQ_Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("****Receive Messages*****");

            var rabbitMqClient = new RabbitMqClient();
            rabbitMqClient.Listen();
            Console.ReadKey();
        }
    }
}
