using System;
using System.Collections.Generic;

namespace Shovel.WebAPI.Models;

public partial class Permission
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Type { get; set; }

    public virtual ICollection<RolePermission> RolePermissions { get; } = new List<RolePermission>();
}
