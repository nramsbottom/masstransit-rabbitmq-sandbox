using Contracts;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace Subscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(x =>
            {

                var host = x.Host(new Uri("rabbitmq://localhost/"), config =>
                {
                    config.Username("guest");
                    config.Password("guest");
                });

                x.ReceiveEndpoint(host, "QueueTestQueue", e =>
                {
                    e.Consumer<Subscriber.Consumers.SomethingHappenedConsumer>();
                });
            });

            bus.Start();
            Console.ReadKey();
            bus.Stop();
        }
    }
}

namespace Subscriber.Consumers
{
    public class SomethingHappenedConsumer : IConsumer<Contracts.SomethingHappened>
    {
        public Task Consume(ConsumeContext<SomethingHappened> context)
        {
            Console.WriteLine("Consumed the message.");
            return Task.FromResult(0);
        }
    }
}

namespace Subscriber.Messages
{
    public class SomethingHappened : Contracts.SomethingHappened
    {
        public string What { get; set; }

        public DateTime When { get; set; }
    }
}
