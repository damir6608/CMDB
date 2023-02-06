using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shovel.WebAPI.Abstractions.Model.Response;
using Shovel.WebAPI.Services.Configuration.Interfaces;
using Shovel.WebAPI.Services.Data.Interfaces;
using System.Xml.Linq;

namespace Shovel.WebAPI
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConfigurationUIController : ControllerBase
    {
        private readonly IConfigurationService _configurationService;

        public ConfigurationUIController(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        [HttpPost]
        [Route("RunCommand")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<PagedResult>> RunCommand()
        {
            String command = HttpContext.Request.Form.Where(i => i.Key == "command").ToList().FirstOrDefault().Value.FirstOrDefault();

            _configurationService.SendCommand(command);

            return Ok();
        }
    }
}
