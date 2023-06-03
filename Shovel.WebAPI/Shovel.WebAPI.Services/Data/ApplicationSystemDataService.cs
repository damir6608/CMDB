using Microsoft.EntityFrameworkCore;
using Shovel.WebAPI.Abstractions.Model;
using Shovel.WebAPI.DataAccess.Extensions;
using Shovel.WebAPI.Models;
using Shovel.WebAPI.Services.Data.Interfaces;

namespace Shovel.WebAPI.Services.Data
{
    public class ApplicationSystemDataService : IApplicationSystemDataService
    {
        private readonly ShovelContext _shovelContext;
        public ApplicationSystemDataService( ShovelContext shovelContext) 
        {
            _shovelContext = shovelContext;
        }

        async Task<ApplicationSystem> IApplicationSystemDataService.GetApplicationSystemById(int id)
        {
            DbSet<ApplicationSystem> applicationSystemsDbSet = _shovelContext.Set<ApplicationSystem>();

            List<ApplicationSystem> applicationSystems = await applicationSystemsDbSet
                                                                .Where(x => x.Id == id)
                                                                .Include(i => i.Server)
                                                                .ToListAsync();

            if (applicationSystems is null)
                throw new ArgumentNullException(nameof(applicationSystems));

            return applicationSystems.First();
        }

        async Task<List<ApplicationSystem>> IApplicationSystemDataService.GetApplicationSystems(QueryFilterModel? queryFilter = null)
        {
            DbSet<ApplicationSystem> applicationSystemsDbSet = _shovelContext.Set<ApplicationSystem>();

            var data = await applicationSystemsDbSet
                .Include(i => i.Server).ToListAsync();

            if (queryFilter is not null)
            {
                var applicationFilter = new ApplicationSystem();
                var filterDict = queryFilter.ParsedFilter;
                foreach ( var filter in filterDict )
                {
                    applicationFilter.SetPropValue(filter.Value, filter.Key);
                }

                //if (applicationFilter.Mainwindowtitle is not null)
                //    data = data.Where(item => item.Mainwindowtitle != null 
                //    ? item.Mainwindowtitle.ToLower().Contains(applicationFilter.Mainwindowtitle) : false)
                //        .ToList();

                //if (applicationFilter.Processname is not null)
                //    data = data.Where(item => item.Processname.ToLower().Contains(applicationFilter.Processname)).ToList();
            }

            return data;
        }
    }
}
