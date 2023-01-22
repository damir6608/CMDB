using MaintenanceService.DataAccess.DataModels;
using MaintenanceService.DataAccess.DataServises;
using Microsoft.AspNetCore.Mvc;

namespace MaintenanceService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PerformanceController : ControllerBase
    {
        private readonly ILogger<PerformanceController> _logger;

        public PerformanceController(ILogger<PerformanceController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetPerformanceData")]
        public IEnumerable<PerformanceModel> Get([FromQuery] string[] queryParams)
        {
            return new PerformanceService().GetPerformanceData();
        }
    }
}
