using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shovel.WebAPI.Models;
using Shovel.WebAPI.Services.Synchronize.Interfaces;
using System.Net;

namespace Shovel.WebAPI.Services.Synchronize
{
    public class PerformanceSystemSynchronizeService : IPerformanceSystemSynchronizeService
    {
        private readonly IHttpClientFactory _factory;
        private readonly ShovelContext _shovelContext;

        public PerformanceSystemSynchronizeService(IServiceProvider serviceProvider, ShovelContext shovelContext)
        {
            _factory = (IHttpClientFactory)serviceProvider.GetService(typeof(IHttpClientFactory));
            _shovelContext = shovelContext;
        }
        async Task<List<PerformanceSystem>> IPerformanceSystemSynchronizeService.GetData()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            List<PerformanceSystem> perfrormance = new List<PerformanceSystem>();
            DbSet<Server> serverSet = _shovelContext.Set<Server>();
            foreach(var server in serverSet)
            {
                DateTime SyncTime = DateTime.Now;
                using (HttpClient client = _factory.CreateClient())
                {
                    client.BaseAddress = new Uri(server.Baseaddress);

                    DateTime dateByLastSync = DateTime.Now.AddMinutes(-10);
                    string serDate = JsonConvert.SerializeObject(dateByLastSync);
                    string param = $"?date={dateByLastSync}";

                    HttpResponseMessage responce = await client.GetAsync($"Performance{param}");
                    string stringData = await responce.Content.ReadAsStringAsync();
                    List<PerformanceSystem> performanceResult = JsonConvert.DeserializeObject<List<PerformanceSystem>>(stringData);

                    performanceResult = performanceResult ?? new List<PerformanceSystem>();

                    DbSet<PerformanceSystem> show = _shovelContext.Set<PerformanceSystem>();

                    performanceResult.ForEach(i => { i.Synctime = SyncTime; i.Serverid = server.Id; });

                    show.AddRange(performanceResult);

                    _shovelContext.SaveChanges();


                    perfrormance.AddRange(performanceResult);
                }
            }
            return perfrormance.ToList();
        }
    }
}
