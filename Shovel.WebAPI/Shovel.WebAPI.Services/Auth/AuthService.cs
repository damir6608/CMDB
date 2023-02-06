using Microsoft.EntityFrameworkCore;
using Shovel.WebAPI.Models;
using Shovel.WebAPI.Services.Auth.Interfaces;
using Shovel.WebAPI.Services.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shovel.WebAPI.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly ShovelContext _shovelContext;
        public AuthService(ShovelContext shovelContext)
        {
            _shovelContext = shovelContext;
        }
        async Task<User> IAuthService.LogIn(string email, string password)
        {
            DbSet<User> usersDbSet = _shovelContext.Set<User>();

            List<User> user = await usersDbSet
                                    .Where(x => x.Email == email && x.Password == password)
                                    .Include(i => i.UserRoles)
                                    .ThenInclude(u => u.Role)
                                    .ThenInclude(r => r.RolePermissions)
                                    .ThenInclude(rp => rp.Permission)
                                    .ToListAsync();

            if (user is null || user.Count < 1)
                throw new ApplicationException("Log in failed!");

            return user.FirstOrDefault();
        }
    }
}
