using System;
using System.Net.Http;
using System.Threading.Tasks;
using CheckService.Data;
using CheckService.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CheckService.Services
{
    public class CheckGoogleService : BackgroundService
    {
        public CheckGoogleService(IServiceScopeFactory scopeFactory) : base(scopeFactory)
        {
        }

        protected override async Task Process(ApplicationDbContext dbContext)
        {
            var googleResult = new GoogleResult();
            googleResult.DateTime = DateTime.Now;
            dbContext.GoogleResults.Add(googleResult);
            try
            {
                var client = new HttpClient();
                var response = await client.GetAsync("https://google.com");

                response.EnsureSuccessStatusCode();
                googleResult.Result = "OK";
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                googleResult.Result = "ERROR";
                googleResult.Error = ex.ToString();
                await dbContext.SaveChangesAsync();
            }
        }
    }
}