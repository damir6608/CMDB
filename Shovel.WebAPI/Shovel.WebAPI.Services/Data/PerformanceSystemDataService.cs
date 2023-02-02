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
