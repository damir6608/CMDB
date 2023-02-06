using MaintenanceService.Configuration.Interfaces;
using System.Diagnostics;

namespace MaintenanceService.Configuration
{
    public class ExecuterCommandConfigurationService : IExecuterCommandConfigurationService
    {
        Task IExecuterCommandConfigurationService.ExecuteCommand(string command)
        {
            return Task.Run(() => 
            {
                Process p = new Process();
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.Arguments = @"/c " + @command;
                p.StartInfo.CreateNoWindow = true;
                p.Start();

            });
            
        }
    }
}
