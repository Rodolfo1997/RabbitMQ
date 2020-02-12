using System;

namespace RabbitMQ_Processor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("****Send Messages*****");

            for (int i = 0; i < 1000; i++)
            {
                var message = string.Concat("Olá Rodolfo ", i, " ", DateTime.Now);

                var rabbitMqConfiguration = new RabbitMqConfiguration();
                rabbitMqConfiguration.Publish(message);

                Console.WriteLine("Send Message = {0}", message);
            }

            Console.ReadKey();
        }
    }
}
