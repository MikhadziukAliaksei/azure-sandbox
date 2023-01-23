using Microsoft.AspNetCore.Mvc;

namespace AzureTest.API.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly TestConfig _testConfig;

    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration configuration, TestConfig testConfig)
    {
        _logger = logger;
        _configuration = configuration;
        _testConfig = testConfig;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }

    [HttpGet("name")]
    public async Task<IActionResult> GetName()
    {
        var name = _configuration["MyApp:Settings:Name"];

        var t = _testConfig.Name;
        
        return Ok(t);
    }
}