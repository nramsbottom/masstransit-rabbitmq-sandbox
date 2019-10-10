using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PublishingWebApplication.Models;
using System;
using System.Diagnostics;

namespace PublishingWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index([FromForm] string action)
        {

            PublishMessage();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        private void PublishMessage()
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(x =>
                               x.Host(new Uri("rabbitmq://localhost/"), h =>
                               {
                                   h.Username("guest");
                                   h.Password("guest");
                               }));

            bus.Start();

            bus.Publish<Contracts.SomethingHappened>(new Messages.SomethingHappened()
            {
                What = "A message was sent from ASP.NET",
                When = DateTime.Now
            });

            bus.Stop();
        }

    }
}
