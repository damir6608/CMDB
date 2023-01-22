using MaintenanceService.DataAccess.DataModels;
using MaintenanceService.DataAccess.DataServises;
using Microsoft.AspNetCore.Mvc;

namespace MaintenanceService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        $"Freezing {new DirectoryInfo(@"..\").FullName}", $"Bracing {new DirectoryInfo(@"..\..\..").FullName}", $"Chilly {new DirectoryInfo(@"..\..\..").FullName}", $"Cool {new DirectoryInfo(@"..\..\..").FullName}", $"Mild {new DirectoryInfo(@"..\..\..").FullName}", $"Warm{new DirectoryInfo(@"..\..\..").FullName}", 
            $"Balmy{new DirectoryInfo(@"..\..\..").FullName}", $"Hot{new DirectoryInfo(@"..\..\..").FullName}", $"Sweltering{new DirectoryInfo(@"..\..\..").FullName}", $"Scorching{new DirectoryInfo(@"..\..\..").FullName}"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[0]
            })
            .ToArray();
        }
    }
}