using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shovel.WebAPI.Abstractions.Model.Response;
using Shovel.WebAPI.Services.Auth.Interfaces;
using Shovel.WebAPI.Services.Configuration.Interfaces;
using Shovel.WebAPI.Services.Data.Interfaces;
using System.Xml.Linq;

namespace Shovel.WebAPI
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthUIController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthUIController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet("LogIn/{email}/{password}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<PagedResult>> LogIn(string email, string password)
        { 
            return Ok(await _authService.LogIn(email, password));
        }
    }
}
