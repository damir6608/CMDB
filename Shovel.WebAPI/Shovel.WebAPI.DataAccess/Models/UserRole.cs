using System;
using System.Collections.Generic;

namespace Shovel.WebAPI.Models;

public partial class UserRole
{
    public int Id { get; set; }

    public int? Userid { get; set; }

    public int? Roleid { get; set; }

    public virtual Role? Role { get; set; }

    public virtual User? User { get; set; }
}
