﻿using Microsoft.Extensions.Logging;
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
        private readonly ShovelContext _shovelContext;

        public PerformanceSystemSynchronizeService(IServiceProvider serviceProvider, ShovelContext shovelContext)
        {
            _factory = (IHttpClientFactory)serviceProvider.GetService(typeof(IHttpClientFactory));
            _shovelContext = shovelContext;
        }
        public async Task<List<PerformanceSystem>> GetData()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            var perfrormance = new List<PerformanceSystem>();
            var serverSet = _shovelContext.Set<Server>().ToList();
            foreach(var server in serverSet)
            {
                DateTime SyncTime = DateTime.Now;
                using (var client = _factory.CreateClient())
                {
                    client.BaseAddress = new Uri(server.Baseaddress);
                    var responce = await client.GetAsync("Performance");
                    var stringData = await responce.Content.ReadAsStringAsync();
                    var performanceResult = JsonConvert.DeserializeObject<List<PerformanceSystem>>(stringData);
                    performanceResult = performanceResult ?? throw new ArgumentNullException();
                    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
                    var show = _shovelContext.Set<PerformanceSystem>();

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