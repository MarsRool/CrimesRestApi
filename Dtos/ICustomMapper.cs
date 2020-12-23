using CrimesRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrimesRestApi.Dtos
{
    public interface ICustomMapper
    {
        CrimeDto Map(Crime crime);
        Crime Map(CrimeDto crimeDto);
    }
}
