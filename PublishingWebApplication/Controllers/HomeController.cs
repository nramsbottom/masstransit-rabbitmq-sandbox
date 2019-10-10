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
        private readonly IBusControl _bus;

        public HomeController(ILogger<HomeController> logger, IBusControl bus)
        {
            _logger = logger;
            _bus = bus;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index([FromForm] string action)
        {

            _bus.Publish<Contracts.SomethingHappened>(new Messages.SomethingHappened()
            {
                What = "A message was sent from ASP.NET",
                When = DateTime.Now
            });

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

    }
}
