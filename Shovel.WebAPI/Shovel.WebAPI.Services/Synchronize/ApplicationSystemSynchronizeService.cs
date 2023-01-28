using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Shovel.WebAPI.Abstractions.Extensions;
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
            var perfrormance = new List<ApplicationSystem>();
            var serverSet = _shovelContext.Set<Server>().ToList();
            foreach(var server in serverSet)
            {
                DateTime SyncTime = DateTime.Now;
                using (var client = _factory.CreateClient())
                {
                    client.BaseAddress = new Uri(server.Baseaddress);

                    var dateByLastSync = DateTime.Now.AddHours(-3);
                    var serDate = JsonConvert.SerializeObject(dateByLastSync);
                    var param = $"?date={dateByLastSync}";

                    var responce = await client.GetAsync($"Application{param}");
                    var stringData = await responce.Content.ReadAsStringAsync();
                    var applicationResult = JsonConvert.DeserializeObject<List<ApplicationSystem>>(stringData);

                    applicationResult = applicationResult ?? new List<ApplicationSystem>();

                    var show = _shovelContext.Set<ApplicationSystem>();

                    applicationResult.ForEach(i => { i.Synctime = SyncTime; i.Serverid = server.Id; });

                    show.AddRange(applicationResult);

                    _shovelContext.SaveChanges();


                    perfrormance.AddRange(applicationResult);
                }
            }
            return perfrormance.ToList();
        }
    }
}
