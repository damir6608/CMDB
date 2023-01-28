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
    public class ApplicationSystemDataService : IApplicationSystemDataService
    {
        private readonly ShovelContext _shovelContext;
        public ApplicationSystemDataService( ShovelContext shovelContext) 
        {
            _shovelContext = shovelContext;
        }

        async Task<List<ApplicationSystem>> IApplicationSystemDataService.GetApplicationSystems()
        {
            DbSet<ApplicationSystem> applicationSystemsDbSet = _shovelContext.Set<ApplicationSystem>();

            return await applicationSystemsDbSet
                .Include(i => i.Server)
                .ToListAsync();
        }
    }
}
