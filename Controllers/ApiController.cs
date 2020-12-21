using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrimesRestApi.Data;
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
        private readonly ICustomMapper _mapper;
        private readonly ICrimesRepo _repo;

        public ApiController(ILogger<ApiController> logger, ICustomMapper mapper, ICrimesRepo repo)
        {
            _logger = logger;
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet("crimes")]
        public async Task<string> GetCrimes(string email, string password)
        {
            try
            {
                User user = await _repo.GetUserCrimes(email, password);
                if (user == null)
                    throw new Exception();
                List<CrimeReadDto> crimeDtos = new List<CrimeReadDto>();                
                foreach (Crime crime in user.Crimes)
                    crimeDtos.Add(_mapper.Map(crime));
                return await Task.FromResult(JsonConvert.SerializeObject(crimeDtos.ToArray(), Formatting.Indented));
            } catch (Exception) {
                return await Task.FromResult("[]");
            }
            /*return JsonConvert.SerializeObject(Enumerable.Range(1, 5).Select(index => new CrimeReadDto
            {
                UUID = "123e4567-e89b-42d3-a456-55664244000" + (char)('0' + index),
                Title = "ljlsdkfjlskdjf" + index,
                Date = new DateTimeOffset(DateTime.Now.AddDays(index)).ToUnixTimeMilliseconds(),
                Solved = index % 2 == 1,
                RequirePolice = index % 2 + 1 == 1,
            })
            .ToArray(), Formatting.Indented);*/
        }
    }
}
