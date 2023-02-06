using MaintenanceService.Configuration.Interfaces;
using MaintenanceService.DataAccess.DataModels;
using MaintenanceService.DataAccess.DataServises;
using Microsoft.AspNetCore.Mvc;

namespace MaintenanceService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExecuteController : ControllerBase
    {
        private readonly ILogger<ApplicationController> _logger;
        private readonly IExecuterCommandConfigurationService _executerCommandConfigurationService;

        public ExecuteController(ILogger<ApplicationController> logger, IExecuterCommandConfigurationService executerCommandConfigurationService)
        {
            _logger = logger;
            _executerCommandConfigurationService = executerCommandConfigurationService;
        }

        [HttpPost(Name = "Command")]
        public async Task Get([FromBody] string command)
        {
            await _executerCommandConfigurationService.ExecuteCommand(command);
        }
    }
}
