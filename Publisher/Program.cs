using MassTransit;
using System;

namespace Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(x =>
                    x.Host(new Uri("rabbitmq://localhost/"), h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    }));

            bus.Start();

            while (true)
            {

                bus.Publish<Contracts.SomethingHappened>(new Messages.SomethingHappened()
                {
                    What = "A message was sent.",
                    When = DateTime.Now
                });

                Console.WriteLine("Message sent. Press any key to send another.");
                Console.ReadKey();
            }

            bus.Stop();
        }
    }
}

namespace Publisher.Messages
{
    public class SomethingHappened : Contracts.SomethingHappened
    {
        public string What { get; set; }

        public DateTime When { get; set; }
    }
}
