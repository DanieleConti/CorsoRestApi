using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Services;
using WebApplication2.Services2;

namespace WebApplication2.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly IGuidService _guidService;
    private readonly IGuidService _guidService2;
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IRandomInt _random1;
    private readonly IRandomInt _random2;

    public WeatherForecastController(ILogger<WeatherForecastController> logger,
                                     IGuidService guidService,
                                     IGuidService guidService2,
                                     IRandomInt random1,
                                     IRandomInt random2)
    {
        _guidService = guidService; 
        _guidService2 = guidService2;
        _logger = logger;
        _random1 = random1;
        _random2 = random2;
    }

    [HttpGet("GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Id = Guid.NewGuid(),
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpGet("GetGuid")]
    public IEnumerable<Guid> GetGuid()
    {
        return [_guidService.GetGuid(), _guidService2.GetGuid()];
    }

    [HttpGet("GetInt")]
    public IEnumerable<int> GetInt()
    {
        return [_random1.GetRandomInt(), _random2.GetRandomInt()];
    }

    [HttpPost("PostInt")]
    public IEnumerable<int> PostInt()
    {
        return [_random1.GetRandomInt(), _random2.GetRandomInt()];
    }
}
