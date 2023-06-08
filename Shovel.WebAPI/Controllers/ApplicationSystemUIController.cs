using ClosedXML.Excel;
using ClosedXML.Extensions;
using Microsoft.AspNetCore.Mvc;
using Shovel.WebAPI.Abstractions.Model;
using Shovel.WebAPI.Abstractions.Model.Response;
using Shovel.WebAPI.Models;
using Shovel.WebAPI.Services.Data.Interfaces;
using Shovel.WebAPI.Services.Report;
using System.IO;

namespace Shovel.WebAPI.Controllers
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

        /// <summary>
        /// Returns a list of applications by filters.
        /// </summary>
        /// <param name="queryParams"> The set of filters. </param>
        /// <returns> OkObjectResult </returns>
        [HttpGet("GetAll")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<PagedResult>> GetApplications([FromQuery] QueryFilterModel queryParams)
        {
            return Ok(await _applicationSystemDataService.GetApplicationSystemsPaged(queryParams));
        }

        /// <summary>
        /// Returns a application item by id.
        /// </summary>
        /// <param name="id"> The id of application. </param>
        /// <returns></returns>
        [HttpGet("GetApplicationById/{id}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<PagedResult>> GetApplicationeById(int id)
        {
            return Ok(await _applicationSystemDataService.GetApplicationSystemById(id));
        }

        /// <summary>
        /// Returns a report in excel format.
        /// </summary>
        [HttpGet("GetReport")]
        [ProducesResponseType(200)]
        public async Task<FileResult> GetReport()
        {
            return await new ApplicationReportService(_applicationSystemDataService).GetReport();
        }
    }
}
