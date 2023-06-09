using Shovel.WebAPI.Abstractions.Model;
using Shovel.WebAPI.Abstractions.Model.Response;
using Shovel.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shovel.WebAPI.Services.Data.Interfaces
{
    public interface IPerformanceSystemDataService
    {
        public Task<List<PerformanceSystem>> GetPerformanceSystems(QueryFilterModel? queryFilter = null);
        public Task<PerformanceSystem> GetPerformanceSystemById(int id);
        public Task<PagedResult> GetPerformanceSystemsPaged(QueryFilterModel? queryFilter = null);
    }
}
