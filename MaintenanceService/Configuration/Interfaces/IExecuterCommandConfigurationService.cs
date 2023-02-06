namespace MaintenanceService.Configuration.Interfaces
{
    public interface IExecuterCommandConfigurationService
    {
        public Task ExecuteCommand(string command);
    }
}
