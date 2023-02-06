using Shovel.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shovel.WebAPI.Services.Configuration.Interfaces
{
    public interface IConfigurationService
    {
        public Task SendCommand(string command);
    }
}
