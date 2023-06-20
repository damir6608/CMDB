using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shovel.WebAPI.Models;
using Shovel.WebAPI.Services.Configuration.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Shovel.WebAPI.Services.Configuration
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IHttpClientFactory _factory;
        private readonly ShovelContext _shovelContext;

        public ConfigurationService(IServiceProvider serviceProvider, ShovelContext shovelContext)
        {
            _factory = (IHttpClientFactory)serviceProvider.GetService(typeof(IHttpClientFactory));
            _shovelContext = shovelContext;
        }
        async Task IConfigurationService.SendCommand(string command)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            List<Server> serverSet = _shovelContext.Set<Server>().ToList();
            foreach (var server in serverSet)
            {
                using (HttpClient client = _factory.CreateClient())
                {
                    client.BaseAddress = new Uri(server.Baseaddress);

                    string param = $"?command={command}";

                    HttpResponseMessage responce = await client.GetAsync($"Execute{param}");
                    string stringData = await responce.Content.ReadAsStringAsync();
                }
            }
        }
    }
}
