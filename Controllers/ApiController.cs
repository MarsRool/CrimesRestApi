using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrimesRestApi.Dtos;
using CrimesRestApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CrimesRestApi.Controllers
{
    [ApiController]
    [Route("api")]
    public class ApiController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<ApiController> _logger;

        public ApiController(ILogger<ApiController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("crimes")]
        public string GetCrimes()
        {
            return JsonConvert.SerializeObject(Enumerable.Range(1, 5).Select(index => new CrimeReadDto
            {
                UUID = "123e4567-e89b-42d3-a456-55664244000" + (char)('0' + index),
                Title = "ljlsdkfjlskdjf" + index,
                Date = DateTime.Now.AddDays(index).ToFileTimeUtc(),
                Solved = index % 2 == 1,
                RequirePolice = index % 2 + 1 == 1,
            })
            .ToArray(), Formatting.Indented);
        }
    }
}
