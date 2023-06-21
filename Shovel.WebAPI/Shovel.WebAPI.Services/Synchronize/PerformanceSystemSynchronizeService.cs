using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shovel.WebAPI.Models;
using Shovel.WebAPI.Services.Synchronize.Interfaces;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

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
            List<Server> serverSet = _shovelContext.Set<Server>().ToList();
            foreach(var server in serverSet)
            {
                using (var handler = new HttpClientHandler())
                {
                    // allow the bad certificate
                    handler.ServerCertificateCustomValidationCallback = (request, cert, chain, errors) => true;

                    DateTime SyncTime = DateTime.UtcNow;
                    using (HttpClient client = new HttpClient(handler))
                    {
                        client.BaseAddress = new Uri(server.Baseaddress);

                        DateTime dateByLastSync = DateTime.UtcNow.AddHours(-2);
                        string serDate = JsonConvert.SerializeObject(dateByLastSync);
                        string param = $"?date={dateByLastSync}";

                        ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
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
            }
            return perfrormance.ToList();
        }
    }
}
