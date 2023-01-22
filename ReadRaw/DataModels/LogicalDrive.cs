
namespace ReadRaw.DataModels
{
    public class LogicalDrive
    {
        public string Drive { get; set; } = string.Empty;
        public string VolumeLabel { get; set; } = string.Empty;
        public string DriveType { get; set; } = string.Empty;
        public string DriveFormat { get; set; } = string.Empty;
        public long TotalSize { get; set; }
        public long AvailableFreeSpace { get; set; }
    }
}
