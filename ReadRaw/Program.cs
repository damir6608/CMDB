// See https://aka.ms/new-console-template for more information
using System.Management;
using System.Text;

namespace ReadRaw // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            SystemInformation();
        }
        public static void SystemInformation()
        {
            StringBuilder systemInfo = new StringBuilder(string.Empty);

            systemInfo.AppendFormat("Operation System:  {0}\n", Environment.OSVersion);
            systemInfo.AppendFormat("Processor Architecture:  {0}\n", Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE"));
            systemInfo.AppendFormat("Processor Model:  {0}\n", Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER"));
            systemInfo.AppendFormat("Processor Level:  {0}\n", Environment.GetEnvironmentVariable("PROCESSOR_LEVEL"));
            systemInfo.AppendFormat("SystemDirectory:  {0}\n", Environment.SystemDirectory);
            systemInfo.AppendFormat("ProcessorCount:  {0}\n", Environment.ProcessorCount);
            systemInfo.AppendFormat("UserDomainName:  {0}\n", Environment.UserDomainName);
            systemInfo.AppendFormat("UserName: {0}n\n", Environment.UserName);
            //Drives
            systemInfo.AppendFormat("LogicalDrives:");
            foreach (System.IO.DriveInfo DriveInfo1 in System.IO.DriveInfo.GetDrives())
            {
                try
                {
                    systemInfo.AppendFormat("\t Drive: {0}ntt VolumeLabel: " +
                        "{1}\t DriveType: {2}ntt DriveFormat: {3}\t " +
                        "TotalSize: {4}\t AvailableFreeSpace: {5}\n",
                        DriveInfo1.Name, DriveInfo1.VolumeLabel, DriveInfo1.DriveType,
                        DriveInfo1.DriveFormat, DriveInfo1.TotalSize, DriveInfo1.AvailableFreeSpace);
                }
                catch
                {
                }
            }
            systemInfo.AppendFormat("Version:  {0}", Environment.Version);
            Console.WriteLine(systemInfo);
            Console.ReadKey();
        }
    }
}
