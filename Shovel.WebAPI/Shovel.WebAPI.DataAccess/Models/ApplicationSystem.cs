using System;
using System.Collections;
using System.Collections.Generic;

namespace Shovel.WebAPI.Models;

public partial class ApplicationSystem
{
    public int Id { get; set; }

    public int? Basepriority { get; set; }

    public Boolean? Hasexited { get; set; }

    public DateTime? Starttime { get; set; }

    public string? Machinename { get; set; }

    public int? Maxworkingset { get; set; }

    public int? Minworkingset { get; set; }

    public long? Nonpagedsystemmemorysize64 { get; set; }

    public long? Pagedmemorysize64 { get; set; }

    public long? Pagedsystemmemorysize64 { get; set; }

    public long? Peakworkingset64 { get; set; }

    public long? Peakvirtualmemorysize64 { get; set; }

    public Boolean? Priorityboostenabled { get; set; }

    public string? Processname { get; set; }

    public string? Startinfofilename { get; set; }

    public int? Threadscount { get; set; }

    public long? Workingset64 { get; set; }

    public string? Mainwindowtitle { get; set; }

    public DateTime? Synctime { get; set; }

    public int? Serverid { get; set; }

    public virtual Server? Server { get; set; }
}
