using ReadRaw.DataModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadRaw.SystemData
{
    internal class ApplicationSystemFiller
    {
        public static List<ApplicationSystemModel> GetApplicationSystems()
        {
            List<ApplicationSystemModel> applicationSystems = new List<ApplicationSystemModel>();
            DateTime insertDate = DateTime.Now;

            foreach(var processes in Process.GetProcesses()
                .Where(i => i.MainWindowTitle != string.Empty))
            {
                applicationSystems.Add(new ApplicationSystemModel()
                {
                    BasePriority = processes.BasePriority,
                    HasExited = processes.HasExited,
                    StartTime = processes.StartTime,
                    MachineName = processes.MachineName,
                    MaxWorkingSet = (int?)processes.MaxWorkingSet,
                    MinWorkingSet = (int?)processes.MinWorkingSet,
                    NonPagedSystemMemorySize64 = processes.NonpagedSystemMemorySize64,
                    PagedMemorySize64 = processes.PagedMemorySize64,
                    PagedSystemMemorySize64 = processes.PagedSystemMemorySize64,
                    PeakWorkingSet64 = processes.PeakWorkingSet64,
                    PeakVirtualMemorySize64 = processes.PeakVirtualMemorySize64,
                    PriorityBoostEnabled = processes.PriorityBoostEnabled,
                    ProcessName = processes.ProcessName,
                    ThreadsCount = processes.Threads.Count,
                    WorkingSet64 = processes.WorkingSet64,
                    MainWindowTitle = processes.MainWindowTitle,
                    InsertDate = insertDate,
                });
            }

            return applicationSystems;
        }
    }
}
