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
        private readonly ILogger<ApiController> _logger;
        private readonly ICustomMapper _mapper;
        private readonly ICrimesRepo _repo;

        public ApiController(ILogger<ApiController> logger, ICustomMapper mapper, ICrimesRepo repo)
        {
            _logger = logger;
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet("register")]
        public async Task<IActionResult> RegisterUser(string email, string password)
        {
            return await RegisterUserPost(email, password);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUserPost(string email, string password)
        {
            bool result = await _repo.RegisterUser(new User
            {
                Email = email,
                Password = password
            });
            if (result)
            {
                await _repo.SaveChanges();
            }
            return RedirectToAction(nameof(GetResult), new { result });
        }

        [HttpGet("crime_result")]
        public async Task<IActionResult> GetResult(bool result)
        {
            return await Task.FromResult(Ok(result ? "{result:true}" : "{result:false}"));
        }

        [HttpGet("crimes")]
        public async Task<IActionResult> GetCrimes(string email, string password)
        {
            try
            {
                User user = await _repo.GetUserCrimes(email, password);
                if (user == null)
                    throw new Exception();
                List<CrimeDto> crimeDtos = new List<CrimeDto>();                
                foreach (Crime crime in user.Crimes)
                    crimeDtos.Add(_mapper.Map(crime));
                return await Task.FromResult(Ok(JsonConvert.SerializeObject(crimeDtos.ToArray(), Formatting.Indented)));
                /*return await Task.FromResult(Ok(JsonConvert.SerializeObject(Enumerable.Range(1, 5).Select(index => new CrimeReadDto
                {
                    UUID = "123e4567-e89b-42d3-a456-55664244000" + (char)('0' + index),
                    Title = "ljlsdkfjlskdjf" + index,
                    Date = new DateTimeOffset(DateTime.Now.AddDays(index)).ToUnixTimeMilliseconds(),
                    Solved = index % 2 == 1,
                    RequirePolice = index % 2 + 1 == 1,
                })
                .ToArray(), Formatting.Indented)));*/
            }
            catch (Exception)
            {
                return await Task.FromResult(Ok("[]"));
            }
        }

        [HttpGet("set_crimes")]
        public async Task<IActionResult> SetCrimes(string email, string password, string crimes)
        {
            return await SetCrimesPost(email, password, crimes);
        }

        [HttpPost("set_crimes")]
        public async Task<IActionResult> SetCrimesPost(string email, string password, string crimes)
        {
            User user = await _repo.GetUserCrimes(email, password);
            if (user == null)
            {
                return RedirectToAction(nameof(GetResult), new { result = false });
            }
            try
            {
                List<CrimeDto> crimeDtos = JsonConvert.DeserializeObject<List<CrimeDto>>(crimes);
                List<Crime> crimesList = new List<Crime>();
                foreach(CrimeDto crimeDto in crimeDtos)
                {
                    crimesList.Add(_mapper.Map(crimeDto));
                }
                bool result = await _repo.SetCrimes(user, crimesList);
                if (result)
                {
                    await _repo.SaveChanges();
                }
                return RedirectToAction(nameof(GetResult), new { result });
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(GetResult), new { result = false });
            }
        }
    }
}
