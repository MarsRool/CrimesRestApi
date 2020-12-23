using CrimesRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrimesRestApi.Dtos
{
    public class CustomMapper : ICustomMapper
    {
        public CrimeDto Map(Crime crime)
        {
            return new CrimeDto
            {
                UUID = crime.UUID,
                Title = crime.Title,
                Date = new DateTimeOffset(crime.Date).ToUnixTimeMilliseconds(),
                Solved = crime.Solved,
                RequirePolice = crime.RequirePolice
            };
        }

        public Crime Map(CrimeDto crimeDto)
        {
            return new Crime
            {
                UUID = crimeDto.UUID,
                Title = crimeDto.Title,
                Date = DateTimeOffset.FromUnixTimeMilliseconds(crimeDto.Date).DateTime.ToLocalTime(),
                Solved = crimeDto.Solved,
                RequirePolice = crimeDto.RequirePolice
            };
        }
    }
}
