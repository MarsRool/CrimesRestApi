using CrimesRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrimesRestApi.Data
{
    public interface ICrimesRepo
    {
        protected static readonly Task<bool> falseTask = Task.FromResult(false);
        protected static readonly Task<bool> trueTask = Task.FromResult(true);

        Task<User> GetUserCrimes(string email, string password);
        Task<bool> RegisterUser(User user);
        
        Task<bool> SetCrimes(User user, List<Crime> crimes);

        Task<bool> SaveChanges();
    }
}
