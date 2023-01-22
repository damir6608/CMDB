using System;
using System.Collections;
using System.Collections.Generic;

namespace Shovel.WebAPI.Models;

public partial class PerformanceSystem
{
    public int Id { get; set; }

    public string? Operationsystem { get; set; }

    public string? Processorarchitecture { get; set; }

    public string? Processormodel { get; set; }

    public string? Processorlevel { get; set; }

    public string? Systemdirectory { get; set; }

    public int? Processorcount { get; set; }

    public string? Userdomainname { get; set; }

    public string? Username { get; set; }

    public string? Machinename { get; set; }

    public int? Processid { get; set; }

    public string? Processpath { get; set; }

    public int? Systempagesize { get; set; }

    public long? Tickcount64 { get; set; }

    public Boolean? Userinteractive { get; set; }

    public long? Workingset { get; set; }

    public Boolean? Is64bitoperatingsystem { get; set; }

    public Boolean? Is64bitprocess { get; set; }

    public double? Ramavailable { get; set; }

    public string? Version { get; set; }

    public DateTime? Insertdate { get; set; }

    public DateTime? Synctime { get; set; }

    public int? Serverid { get; set; }

    public virtual ICollection<LogicalDrive> LogicalDrives { get; } = new List<LogicalDrive>();

    public virtual Server? Server { get; set; }
}
