using Quartz;

namespace ReadRaw.QuartzJobs
{
    internal class SimpleJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Task.Run(() => Run());
        }

        public void Run()
        {
            DataAccess.DataService.InsertPerformanceData();
            DataAccess.DataService.InsertApplicationData();

            Console.WriteLine("Logging at :- " + DateTime.Now);
        }
    }
}