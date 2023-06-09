using Microsoft.EntityFrameworkCore;
using Shovel.WebAPI.Abstractions.Model;
using Shovel.WebAPI.Abstractions.Model.Response;
using Shovel.WebAPI.DataAccess.Extensions;
using Shovel.WebAPI.Models;
using Shovel.WebAPI.Services.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shovel.WebAPI.Services.Data
{
    public class PerformanceSystemDataService : IPerformanceSystemDataService
    {
        private readonly ShovelContext _shovelContext;
        public PerformanceSystemDataService( ShovelContext shovelContext) 
        {
            _shovelContext = shovelContext;
        }

        async Task<PerformanceSystem> IPerformanceSystemDataService.GetPerformanceSystemById(int id)
        {
            DbSet<PerformanceSystem> performanceSystemsDbSet = _shovelContext.Set<PerformanceSystem>();

            List<PerformanceSystem> performanceSystems =  await performanceSystemsDbSet
                                                                .Where(x => x.Id == id)
                                                                .Include(i => i.LogicalDrives)
                                                                .Include(i => i.Server)
                                                                .ToListAsync();

            if (performanceSystems is null)
                throw new ArgumentNullException(nameof(performanceSystems));

            return performanceSystems.FirstOrDefault();
        }

        public async Task<List<PerformanceSystem>> GetPerformanceSystems(QueryFilterModel? queryFilter = null)
        {
            DbSet<PerformanceSystem> performanceSystemsDbSet = _shovelContext.Set<PerformanceSystem>();

            var data = await performanceSystemsDbSet
                .Include(i => i.LogicalDrives)
                .Include(i => i.Server)
                .ToListAsync(); 
            
            if (queryFilter != null)
            {
                IEnumerable<PerformanceSystem> filteredData = data;
                if (!string.IsNullOrWhiteSpace(queryFilter?.Filter))
                {
                    var filterDict = queryFilter.ParsedFilter;
                    foreach (var filter in filterDict)
                    {
                        filteredData = data.Where(filter.Key.CreateContainsExpression<PerformanceSystem>(filter.Value).Compile());
                    }
                }

                filteredData = filteredData.Skip(queryFilter.Skip);
                filteredData = filteredData.Take(queryFilter.Top + 1);

                data = filteredData.ToList();
            }


            return data;
        }

        async Task<PagedResult> IPerformanceSystemDataService.GetPerformanceSystemsPaged(QueryFilterModel? queryFilter = null)
        {
            var data = await GetPerformanceSystems(queryFilter);
            PagedResult res = new PagedResult(data);
            res.TotalCount =  queryFilter.InlineCount == "allpages" ? await GetPerformanceSystemsCount() : data.Count;
            return res;
        }

        private async Task<int> GetPerformanceSystemsCount()
        {
            DbSet<PerformanceSystem> performanceSystemsDbSet = _shovelContext.Set<PerformanceSystem>();

            return await performanceSystemsDbSet.CountAsync();
        }
    }
}
