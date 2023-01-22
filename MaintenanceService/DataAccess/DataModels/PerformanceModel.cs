
namespace MaintenanceService.DataAccess.DataModels
{
    public class PerformanceModel
    {
        public string OperationSystem { get; set; } = string.Empty;
        public string? ProcessorArchitecture { get; set; } = string.Empty;
        public string? ProcessorModel { get; set; } = string.Empty;
        public string? ProcessorLevel { get; set; } = string.Empty;
        public string SystemDirectory { get; set; } = string.Empty;
        public int ProcessorCount { get; set; }
        public string UserDomainName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string MachineName { get; set; } = string.Empty;
        public int ProcessId { get; set; }
        public string? ProcessPath { get; set; } = string.Empty;
        public int SystemPageSize { get; set; }
        public long TickCount64 { get; set; }
        public bool UserInteractive { get; set; }
        public long WorkingSet { get; set; }
        public bool Is64BitOperatingSystem { get; set; }
        public bool Is64BitProcess { get; set; }
        public List<LogicalDrive>? LogicalDrives { get; set; }
        public double RAMAvailable { get; set; }
        public string Version { get; set; } = string.Empty;
        public DateTime InsertDate { get; set; }

    }
}
