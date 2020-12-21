using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrimesRestApi.Data;
using CrimesRestApi.Dtos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CrimesRestApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
            Environment.EnvironmentName = "Production";
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<CrimeDbContext>(opt => opt.UseNpgsql(
                Configuration.GetConnectionString(
                    Environment.IsDevelopment() ? "CrimeDevConnection" : "CrimeConnection"),
                options =>
                {
                    options.SetPostgresVersion(new Version("9.5"));
                }));

            services.AddScoped<ICrimesRepo, CrimesRepo>();
            services.AddSingleton<ICustomMapper, CustomMapper>();
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Api}/{action=GetCrimes}");
            });
        }
    }
}
