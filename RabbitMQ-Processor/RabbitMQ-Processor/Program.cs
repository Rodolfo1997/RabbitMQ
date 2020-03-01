using System;

namespace RabbitMQ_Processor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("****Send Messages*****");
            var rabbitMqConfiguration = new RabbitMqConfiguration();

            for (int i = 0; i < 100000; i++)
            {
                var message = string.Concat("Olá Rodolfo ", i, " ", DateTime.Now);

                rabbitMqConfiguration.Publish(message);

                Console.WriteLine("Send Message = {0}", message);
            }

            Console.ReadKey();
        }
    }
}
