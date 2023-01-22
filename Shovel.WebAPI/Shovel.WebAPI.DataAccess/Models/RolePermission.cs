using System;
using System.Collections.Generic;

namespace Shovel.WebAPI.Models;

public partial class RolePermission
{
    public int Id { get; set; }

    public int? Permissionid { get; set; }

    public int? Roleid { get; set; }

    public virtual Permission? Permission { get; set; }

    public virtual Role? Role { get; set; }
}
