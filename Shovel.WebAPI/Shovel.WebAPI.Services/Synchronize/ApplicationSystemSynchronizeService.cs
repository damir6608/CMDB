using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shovel.WebAPI.Models;
using Shovel.WebAPI.Services.Synchronize.Interfaces;
using System.Net;

namespace Shovel.WebAPI.Services.Synchronize
{
    public class ApplicationSystemSynchronizeService : IApplicationSystemSynchronizeService
    {
        private readonly IHttpClientFactory _factory;
        private readonly ShovelContext _shovelContext;

        public ApplicationSystemSynchronizeService(IServiceProvider serviceProvider, ShovelContext shovelContext)
        {
            _factory = (IHttpClientFactory)serviceProvider.GetService(typeof(IHttpClientFactory));
            _shovelContext = shovelContext;
        }
        async Task<List<ApplicationSystem>> IApplicationSystemSynchronizeService.GetData()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            List<ApplicationSystem> perfrormance = new List<ApplicationSystem>();
            List<Server> serverSet = _shovelContext.Set<Server>().ToList();
            foreach(Server server in serverSet)
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

                        HttpResponseMessage responce = await client.GetAsync($"Application{param}");
                        string stringData = await responce.Content.ReadAsStringAsync();
                        List<ApplicationSystem> applicationResult = JsonConvert.DeserializeObject<List<ApplicationSystem>>(stringData);

                        applicationResult = applicationResult ?? new List<ApplicationSystem>();

                        DbSet<ApplicationSystem> show = _shovelContext.Set<ApplicationSystem>();

                        applicationResult.ForEach(i => { i.Synctime = SyncTime; i.Serverid = server.Id; });

                        show.AddRange(applicationResult);

                        _shovelContext.SaveChanges();


                        perfrormance.AddRange(applicationResult);
                    }
                }
            }
            return perfrormance.ToList();
        }
    }
}
