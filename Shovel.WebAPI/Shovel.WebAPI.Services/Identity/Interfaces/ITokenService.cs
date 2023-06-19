using Microsoft.AspNetCore.Identity;
using Shovel.WebAPI.DataAccess.Models.Web;

namespace Shovel.WebAPI.Services.Identity.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(ApplicationUser user, List<IdentityRole<long>> role);
    }
}
