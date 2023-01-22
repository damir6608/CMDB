using ReadRaw.DataModels;
using System.Diagnostics;

namespace ReadRaw.SystemData
{
    internal class PerformanceDataFiller
    {
        public static PerformanceModel GetPerformanceCounter()
        {
            PerformanceModel performanceModel = new PerformanceModel()
            {
                OperationSystem = Environment.OSVersion.ToString(),
                ProcessorArchitecture = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE"),
                ProcessorModel = Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER"),
                ProcessorLevel = Environment.GetEnvironmentVariable("PROCESSOR_LEVEL"),
                SystemDirectory = Environment.SystemDirectory,
                ProcessorCount = Environment.ProcessorCount,
                UserDomainName = Environment.UserDomainName,
                UserName = Environment.UserName,
                MachineName = Environment.MachineName,
                ProcessId = Environment.ProcessId,
                ProcessPath = Environment.ProcessPath,
                SystemPageSize = Environment.SystemPageSize,
                TickCount64 = Environment.TickCount64,
                UserInteractive = Environment.UserInteractive,
                WorkingSet = Environment.WorkingSet,
                Is64BitOperatingSystem = Environment.Is64BitOperatingSystem,
                Is64BitProcess = Environment.Is64BitProcess,
                Version = Environment.Version.ToString()
            };

            performanceModel.LogicalDrives = new List<LogicalDrive>();
            //Drives
            foreach (System.IO.DriveInfo DriveInfo1 in System.IO.DriveInfo.GetDrives())
            {
                try
                {
                    performanceModel.LogicalDrives.Add(new LogicalDrive()
                    {
                        Drive = DriveInfo1.Name,
                        VolumeLabel = DriveInfo1.VolumeLabel,
                        DriveType = DriveInfo1.DriveType.ToString(),
                        DriveFormat = DriveInfo1.DriveFormat,
                        TotalSize = DriveInfo1.TotalSize,
                        AvailableFreeSpace = DriveInfo1.AvailableFreeSpace
                    });
                }
                catch
                {
                }
            }

            GCMemoryInfo gcMemoryInfo = GC.GetGCMemoryInfo();
            Int64 installedMemory = gcMemoryInfo.TotalAvailableMemoryBytes;
            performanceModel.RAMAvailable = installedMemory;

            return performanceModel;
        }
    }
}
