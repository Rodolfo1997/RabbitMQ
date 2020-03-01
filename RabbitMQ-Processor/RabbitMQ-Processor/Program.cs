using System;

namespace RabbitMQ_Processor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("****Send Messages*****");
            var rabbitMqClient = new RabbitMqClient();

            for (int i = 0; i < 100000; i++)
            {
                var message = string.Concat("Olá Rodolfo ", i, " ", DateTime.Now);

                rabbitMqClient.Publish(message);

                Console.WriteLine("Send Message = {0}", message);
            }

            Console.ReadKey();
        }
    }
}
