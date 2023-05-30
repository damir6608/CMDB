using ReadRaw.DataModels;
using System.Diagnostics;

namespace ReadRaw.SystemData
{
    internal class ApplicationSystemFiller
    {
        public static List<ApplicationSystemModel> GetApplicationSystems()
        {
            List<ApplicationSystemModel> applicationSystems = new List<ApplicationSystemModel>();
            DateTime insertDate = DateTime.UtcNow;

            IEnumerable<Process> processes = Process.GetProcesses()
                .Where(i => i.MainWindowTitle != string.Empty);

            foreach (Process? process in processes)
            {
                applicationSystems.Add(new ApplicationSystemModel()
                {
                    BasePriority = process.BasePriority,
                    HasExited = process.HasExited,
                    StartTime = process.StartTime,
                    MachineName = process.MachineName,
                    MaxWorkingSet = (int?)process.MaxWorkingSet,
                    MinWorkingSet = (int?)process.MinWorkingSet,
                    NonPagedSystemMemorySize64 = process.NonpagedSystemMemorySize64,
                    PagedMemorySize64 = process.PagedMemorySize64,
                    PagedSystemMemorySize64 = process.PagedSystemMemorySize64,
                    PeakWorkingSet64 = process.PeakWorkingSet64,
                    PeakVirtualMemorySize64 = process.PeakVirtualMemorySize64,
                    PriorityBoostEnabled = process.PriorityBoostEnabled,
                    ProcessName = process.ProcessName,
                    ThreadsCount = process.Threads.Count,
                    WorkingSet64 = process.WorkingSet64,
                    MainWindowTitle = process.MainWindowTitle,
                    InsertDate = insertDate,
                });
            }

            return applicationSystems;
        }
    }
}
