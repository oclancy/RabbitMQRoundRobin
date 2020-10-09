using RabbitMQ.Client;
using System;
using System.Text;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var count = 1;
            var factory = new ConnectionFactory() { HostName = args[0], UserName = "guest", Password="admin" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                do
                {
                    var body = Encoding.UTF8.GetBytes(count.ToString());

                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;

                    channel.BasicPublish(exchange: "amq.topic",
                                         routingKey: $"tasks.{count}",
                                         basicProperties: properties,
                                         body: body);
                    Console.WriteLine(" [x] Sent {0}", count);
                }
                while (count++ < 10);
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }



    }
}
