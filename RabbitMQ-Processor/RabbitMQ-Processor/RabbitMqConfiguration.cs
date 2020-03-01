﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

namespace RabbitMQ_Processor
{
    public class RabbitMqConfiguration : IDisposable
    {
        private readonly string rabbitMqUrl = "amqp://guest:guest@localhost:5672";
        private readonly string queueName = "RabbitMQQueue-Rodolfo-one";
        private readonly IConnection connection;
        private readonly IModel model;

        public RabbitMqConfiguration()
        {
            this.connection = CreateConnection();
            this.model = connection.CreateModel();
        }

        private IConnection CreateConnection()
        {
            var connectionFactory = new ConnectionFactory
            {
                Uri = new Uri(rabbitMqUrl)
            };

            return connectionFactory.CreateConnection();
        }

        private void ConfigureQueue(string name, IModel model)
        {
            model.QueueDeclare(queue: name, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        public void Publish(string message)
        {
            ConfigureQueue(queueName, model);
            model.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: Encoding.UTF8.GetBytes(message));
        }

        public void Listen()
        {
            ConfigureQueue(queueName, model);

            var consumer = new EventingBasicConsumer(model);
            consumer.Received += (sender, @event) =>
            {
                var body = @event.Body;
                var message = Encoding.UTF8.GetString(body);

                if (!string.IsNullOrWhiteSpace(message))
                {
                    model.BasicAck(@event.DeliveryTag, true);
                }

                Console.WriteLine("Received Message = {0}", message);
            };

            model.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
        }

        public void Dispose()
        {
            if (model != null && model.IsOpen)
            {
                model.Close();
                model.Dispose();
            }

            if (connection != null && connection.IsOpen)
            {
                connection.Close();
                connection.Dispose();
            }
        }
    }
}
