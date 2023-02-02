using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shovel.WebAPI.Abstractions.Model.Response;
using Shovel.WebAPI.Models;
using Shovel.WebAPI.Services.Data.Interfaces;

namespace Shovel.WebAPI
{
    [ApiController]
    [Route("api/[controller]")]
    public class PerformanceSystemUIController : ControllerBase
    {
        private readonly IPerformanceSystemDataService _performanceSystemDataService;

        public PerformanceSystemUIController(IPerformanceSystemDataService performanceSystemService)
        {
            _performanceSystemDataService = performanceSystemService;
        }

        [HttpGet("GetAll")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<PagedResult>> GetPerformances([FromQuery] string[] queryParams)
        {
            var performanceSystems = await _performanceSystemDataService.GetPerformanceSystems();

            var res = new PagedResult(performanceSystems);
            res.TotalCount = performanceSystems.Count;

            return Ok(res);
        }

        [HttpGet("GetPerformanceById/{id}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<PagedResult>> GetPerformanceById(int id)
        {
            return Ok(await _performanceSystemDataService.GetPerformanceSystemById(id));
        }
    }
}
