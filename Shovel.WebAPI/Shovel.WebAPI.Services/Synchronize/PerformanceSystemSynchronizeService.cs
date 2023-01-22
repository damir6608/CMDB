using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Shovel.WebAPI.Models;
using Shovel.WebAPI.Services.Synchronize.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Shovel.WebAPI.Services.Synchronize
{
    public class PerformanceSystemSynchronizeService : IPerformanceSystemSynchronizeService
    {
        private readonly IHttpClientFactory _factory;

        public PerformanceSystemSynchronizeService(IServiceProvider serviceProvider)
        {
            _factory = (IHttpClientFactory)serviceProvider.GetService(typeof(IHttpClientFactory));
        }
        public async Task<List<PerformanceSystem>> GetData()
        {
            var perfrormance = new List<PerformanceSystem>();
            {
                using (var client = _factory.CreateClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7129/");
                    var responce = await client.GetAsync("Performance");
                    var stringData = await responce.Content.ReadAsStringAsync();
                    var performanceResult = JsonConvert.DeserializeObject<List<PerformanceSystem>>(stringData);

                    perfrormance.AddRange(performanceResult);
                }
                return perfrormance.ToList();
            }
        }
    }
}
