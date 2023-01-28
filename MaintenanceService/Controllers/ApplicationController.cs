using MaintenanceService.DataAccess.DataModels;
using MaintenanceService.DataAccess.DataServises;
using Microsoft.AspNetCore.Mvc;

namespace MaintenanceService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApplicationController : ControllerBase
    {
        private readonly ILogger<ApplicationController> _logger;

        public ApplicationController(ILogger<ApplicationController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetApplicationData")]
        public IEnumerable<ApplicationSystemModel> Get([FromQuery(Name = "date")] DateTime lastSyncDate)
        {
            return new ApplicationService().GetApplicationData(lastSyncDate);
        }
    }
}
