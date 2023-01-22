using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceService.DataAccess.DataModels
{
    public class ApplicationSystemModel
    {
        public int? BasePriority { get; set; }
        public bool? HasExited { get; set; }
        public DateTime? StartTime { get; set; }
        public string? MachineName { get; set; }
        public int? MaxWorkingSet { get; set; }
        public int? MinWorkingSet { get; set; }
        public long? NonPagedSystemMemorySize64 { get; set; }
        public long? PagedMemorySize64 { get; set; }
        public long? PagedSystemMemorySize64 { get; set; }
        public long? PeakWorkingSet64 { get; set; }
        public long? PeakVirtualMemorySize64 { get; set; }
        public bool? PriorityBoostEnabled { get; set; }
        public string? ProcessName { get; set; }
        public int? ThreadsCount { get; set; }
        public long? WorkingSet64 { get; set; }
        public string? MainWindowTitle { get; set; }
        public DateTime InsertDate { get; set; }
    }
}
