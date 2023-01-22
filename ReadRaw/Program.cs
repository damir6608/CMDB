// See https://aka.ms/new-console-template for more information
using System.Configuration;
using System.Management;
using System.Text;
using Topshelf;

namespace ReadRaw // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TopshelfExitCode rc = HostFactory.Run(x =>
            {
                x.Service<Scheduler>(s =>
                {
                    s.ConstructUsing(name => new Scheduler());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();
                x.SetDescription(ConfigurationManager.AppSettings["ServiceDescription"]);
                x.SetDisplayName(ConfigurationManager.AppSettings["ServiceDisplayName"]);
                x.SetServiceName(ConfigurationManager.AppSettings["ServiceName"]);
            });
            Int32 exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());
            Environment.ExitCode = exitCode;
        }
    }
}
