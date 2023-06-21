using Microsoft.EntityFrameworkCore;
using Shovel.WebAPI.Abstractions.Model;
using Shovel.WebAPI.Abstractions.Model.Response;
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

        public async Task<List<ApplicationSystem>> GetApplicationSystems(QueryFilterModel? queryFilter = null)
        {
            DbSet<ApplicationSystem> applicationSystemsDbSet = _shovelContext.Set<ApplicationSystem>();

            var data = await applicationSystemsDbSet.Include(i => i.Server).ToListAsync();

            if (queryFilter != null)
            {
                IEnumerable<ApplicationSystem> filteredData = data;
                if (!string.IsNullOrWhiteSpace(queryFilter?.Filter))
                {
                    var applicationFilter = new ApplicationSystem();
                    var filterDict = queryFilter.ParsedFilter;
                    foreach (var filter in filterDict)
                    {
                        applicationFilter.SetPropValue(filter.Value, filter.Key);
                        filteredData = data.Where(filter.Key.CreateContainsExpression<ApplicationSystem>(filter.Value).Compile());
                    }
                }

                data = filteredData.ToList();
            }


            return data;
        }

        async Task<PagedResult> IApplicationSystemDataService.GetApplicationSystemsPaged(QueryFilterModel? queryFilter = null)
        {
            var data = await GetApplicationSystems(queryFilter);
            var dataCount = data.Count();

            data = data.Skip(queryFilter.Skip).ToList();
            data = data.Take(queryFilter.Top).ToList();

            PagedResult res = new PagedResult(data);
            res.TotalCount = queryFilter?.ParsedFilter.Count == 0 ? await GetApplicationSystemsCount() : dataCount;
            return res;
        }

        private async Task<int> GetApplicationSystemsCount()
        {
            DbSet<ApplicationSystem> applicationSystemsDbSet = _shovelContext.Set<ApplicationSystem>();

            return await applicationSystemsDbSet.CountAsync();
        }
    }
}
