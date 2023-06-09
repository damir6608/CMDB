using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shovel.WebAPI.Abstractions.Model;
using Shovel.WebAPI.Abstractions.Model.Response;
using Shovel.WebAPI.Models;
using Shovel.WebAPI.Services.Data;
using Shovel.WebAPI.Services.Data.Interfaces;
using Shovel.WebAPI.Services.Report;

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
        public async Task<ActionResult<PagedResult>> GetPerformances([FromQuery] QueryFilterModel queryParams)
        {
            return Ok(await _performanceSystemDataService.GetPerformanceSystemsPaged(queryParams));
        }

        [HttpGet("GetPerformanceById/{id}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<PagedResult>> GetPerformanceById(int id)
        {
            return Ok(await _performanceSystemDataService.GetPerformanceSystemById(id));
        }

        /// <summary>
        /// Returns a report in excel format.
        /// </summary>
        [HttpGet("GetReport")]
        [ProducesResponseType(200)]
        public async Task<FileResult> GetReport()
        {
            return await new PerformanceReportService(_performanceSystemDataService).GetReport();
        }
    }
}
