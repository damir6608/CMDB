using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Shovel.WebAPI.Models;

public partial class Server
{
    public int Id { get; set; }

    public string Baseaddress { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<ApplicationSystem> ApplicationSystems { get; } = new List<ApplicationSystem>();

    [JsonIgnore]
    public virtual ICollection<PerformanceSystem> PerformanceSystems { get; } = new List<PerformanceSystem>();
}
