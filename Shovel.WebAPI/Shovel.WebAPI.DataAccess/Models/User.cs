using System;
using System.Collections.Generic;

namespace Shovel.WebAPI.Models;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Email { get; set; }

    public string Password { get; set; } = null!;

    public string? Phone { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; } = new List<UserRole>();
}
