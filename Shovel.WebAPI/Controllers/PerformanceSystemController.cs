using Microsoft.AspNetCore.Mvc;
using Shovel.WebAPI.Services.Synchronize.Interfaces;

namespace Shovel.WebAPI
{
    public class PerformanceSystemController : ControllerBase
    {
        private readonly IPerformanceSystemSynchronizeService _performanceSystemSynchronizeService;

        public PerformanceSystemController(IPerformanceSystemSynchronizeService performanceSystemService)
        {
            _performanceSystemSynchronizeService = performanceSystemService;
        }

        [HttpGet("SyncPerformance")]
        [ProducesResponseType(200)]
        public async Task<ActionResult> SyncPerformacne()
        {
            return Ok(await _performanceSystemSynchronizeService.GetData());
        }
    }
}
