using CrimesRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrimesRestApi.Dtos
{
    public class CustomMapper : ICustomMapper
    {
        public CrimeReadDto Map(Crime crime)
        {
            return new CrimeReadDto
            {
                Id = crime.Id,
                UUID = crime.UUID,
                Title = crime.Title,
                Date = new DateTimeOffset(crime.Date).ToUnixTimeMilliseconds(),
                Solved = crime.Solved,
                RequirePolice = crime.RequirePolice
            };
        }
    }
}
