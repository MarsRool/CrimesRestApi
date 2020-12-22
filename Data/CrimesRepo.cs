using CrimesRestApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrimesRestApi.Data
{
    public class CrimesRepo : ICrimesRepo
    {
        private readonly CrimeDbContext _context;
        public CrimesRepo(CrimeDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserCrimes(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
            if (user != null)
            {
                _context.Entry(user).Collection(c => c.Crimes).Load();
            }
            return await Task.FromResult(user);
        }

        public async Task<bool> RegisterUser(User user)
        {
            if (user == null || _context.Users.First(u => u.Email == user.Email) != null)
            {
                return await ICrimesRepo.falseTask;
            }
            await _context.AddAsync(user);
            return await ICrimesRepo.trueTask;
        }

        public async Task<bool> SetCrimes(User user, List<Crime> crimes)
        {
            if (user == null)
            {
                return await ICrimesRepo.falseTask;
            }
            if (user.Crimes == null)
            {
                _context.Entry(user).Collection(c => c.Crimes).Load();
            }
            return await SaveCrimes(user, crimes);
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        private async Task<bool> SaveCrimes(User user, List<Crime> crimes)
        {
            List<Crime> crimesCreate = new List<Crime>(),
                crimesUpdate = new List<Crime>(),
                crimesDelete = new List<Crime>();

            foreach (var crime in user.Crimes)
            {
                if (crimes.Contains(crime, Crime.comparer))
                    crimesUpdate.Add(crime);
                else
                    crimesDelete.Add(crime);
            }
            foreach (var crime in crimes)
            {
                if (!user.Crimes.Contains(crime, Crime.comparer))
                    crimesCreate.Add(crime);
            }

            foreach (var crime in crimesDelete)
            {
                try
                {
                    await DeleteCrime(crime);
                } catch (Exception) {

                }
            }
            foreach (var crime in crimesCreate)
            {
                try
                {
                    await CreateCrime(crime);
                } catch (Exception) {

                }
            }
            foreach (var crime in crimesUpdate)
            {
                try
                {
                    await UpdateCrime(crime);
                }
                catch (Exception)
                {

                }
            }
            return await ICrimesRepo.trueTask;
        }

        private async Task<bool> CreateCrime(Crime crime)
        {
            if (crime == null)
            {
                return await ICrimesRepo.falseTask;
            }
            await _context.AddAsync(crime);
            return await ICrimesRepo.trueTask;
        }
        private async Task<bool> UpdateCrime(Crime crime)
        {
            if (crime == null)
            {
                return await ICrimesRepo.falseTask;
            }
            _context.Update(crime);
            return await ICrimesRepo.trueTask;
        }
        private async Task<bool> DeleteCrime(Crime crime)
        {
            if (crime == null)
            {
                return await ICrimesRepo.falseTask;
            }
            _context.Remove(crime);
            return await ICrimesRepo.trueTask;
        }
    }
}
