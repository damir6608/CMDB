using Microsoft.EntityFrameworkCore;
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

        async Task<List<PerformanceSystem>> IPerformanceSystemDataService.GetPerformanceSystems()
        {
            DbSet<PerformanceSystem> performanceSystemsDbSet = _shovelContext.Set<PerformanceSystem>();

            return await performanceSystemsDbSet
                .Include(i => i.LogicalDrives)
                .Include(i => i.Server)
                .ToListAsync();
        }
    }
}
