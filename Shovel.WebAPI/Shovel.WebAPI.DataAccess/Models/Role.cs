using System;
using System.Collections.Generic;

namespace Shovel.WebAPI.Models;

public partial class Role
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<RolePermission> RolePermissions { get; } = new List<RolePermission>();

    public virtual ICollection<UserRole> UserRoles { get; } = new List<UserRole>();
}
