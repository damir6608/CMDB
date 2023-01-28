using System;
using System.Collections.Generic;
using Newtonsoft;
using Newtonsoft.Json;

namespace Shovel.WebAPI.Models;

public partial class LogicalDrive
{
    public int Id { get; set; }

    public string? Drive { get; set; }

    public string? Volumelabel { get; set; }

    public string? Drivetype { get; set; }

    public string? Driveformat { get; set; }

    public long? Totalsize { get; set; }

    public long? Availablefreespace { get; set; }

    public int? Performancesystemid { get; set; }

    [JsonIgnore]
    public virtual PerformanceSystem? Performancesystem { get; set; }
}
