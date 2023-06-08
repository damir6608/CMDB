using Shovel.WebAPI.Abstractions.Model;
using Shovel.WebAPI.Abstractions.Model.Response;
using Shovel.WebAPI.Models;

namespace Shovel.WebAPI.Services.Data.Interfaces
{
    public interface IApplicationSystemDataService
    {
        /// <summary>
        /// Returns the application item by id. 
        /// </summary>
        /// <param name="id"> The id of application. </param>
        public Task<ApplicationSystem> GetApplicationSystemById(int id);

        /// <summary>
        /// Returns the list of applications from the database.
        /// </summary>
        public Task<List<ApplicationSystem>> GetApplicationSystems(QueryFilterModel? queryFilter = null);

        public Task<PagedResult> GetApplicationSystemsPaged(QueryFilterModel? queryFilter = null);
    }
}
