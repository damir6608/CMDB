using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shovel.WebAPI.Abstractions.Model.Response;
using Shovel.WebAPI.Models;
using Shovel.WebAPI.Services.Data;
using Shovel.WebAPI.Services.Data.Interfaces;

namespace Shovel.WebAPI
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationSystemUIController : ControllerBase
    {
        private readonly IApplicationSystemDataService _applicationSystemDataService;

        public ApplicationSystemUIController(IApplicationSystemDataService applicationSystemService)
        {
            _applicationSystemDataService = applicationSystemService;
        }

        [HttpGet("GetAll")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<PagedResult>> GetApplications([FromQuery] string[] queryParams)
        {
            var applicationSystems = await _applicationSystemDataService.GetApplicationSystems();

            var res = new PagedResult(applicationSystems);
            res.TotalCount = applicationSystems.Count;

            return Ok(res);
        }

        [HttpGet("GetApplicationById/{id}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<PagedResult>> GetApplicationeById(int id)
        {
            return Ok(await _applicationSystemDataService.GetApplicationSystemById(id));
        }
    }
}
