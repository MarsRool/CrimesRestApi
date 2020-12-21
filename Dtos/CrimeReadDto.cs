using CrimesRestApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CrimesRestApi.Dtos
{
    public class CrimeReadDto
    {
        public int Id { get; set; }
        public string UUID { get; set; }
        public string Title { get; set; }
        public long Date { get; set; }
        public bool Solved { get; set; }
        public bool RequirePolice { get; set; }
        public User User { get; set; }
    }
}
