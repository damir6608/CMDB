using Shovel.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shovel.WebAPI.Services.Data.Interfaces
{
    public interface IApplicationSystemDataService
    {
        public Task<List<ApplicationSystem>> GetApplicationSystems();
        public Task<ApplicationSystem> GetApplicationSystemById(int id);
    }
}
