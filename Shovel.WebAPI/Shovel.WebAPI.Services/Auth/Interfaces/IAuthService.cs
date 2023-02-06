using Shovel.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shovel.WebAPI.Services.Auth.Interfaces
{
    public interface IAuthService
    {
        public Task<User> LogIn(string email, string password);
    }
}
