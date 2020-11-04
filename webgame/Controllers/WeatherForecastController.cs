using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private IHubContext<AppHub> _hub;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger, 
            IHubContext<AppHub> hub)
        {
            _logger = logger;
            _hub = hub;
        }

        // https://code-maze.com/netcore-signalr-angular/
        [HttpGet]
        [Route("GetWeatherForecast")]
        public IActionResult GetWeatherForecast()
        {
            _hub.Clients.All.SendAsync("broadcastdata2", "данные из контроллера");

            return Ok();
        }

    }
}
