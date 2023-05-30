using Quartz;

namespace ReadRaw.QuartzJobs
{
    internal class InsertingCollectedDataJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Task.Run(() => Run());
        }

        public void Run()
        {
            Console.WriteLine("The data inserting job started at :- " + DateTime.UtcNow);

            DataAccess.DataService.InsertPerformanceData();
            DataAccess.DataService.InsertApplicationData();

            Console.WriteLine("The data was successfully added at :- " + DateTime.UtcNow);
        }
    }
}