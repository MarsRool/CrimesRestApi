using CrimesRestApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrimesRestApi.Data
{
    public class CrimeDbContext : DbContext
    {
        public CrimeDbContext(DbContextOptions<CrimeDbContext> opt)
            : base(opt)
        {
        }

        public DbSet<Crime> Crimes { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Crime>().ToTable("crime");
            modelBuilder.Entity<User>().ToTable("user");

            modelBuilder.Entity<User>()
                .HasMany(c => c.Crimes)
                .WithOne(e => e.User);
            base.OnModelCreating(modelBuilder);
        }
    }
}
