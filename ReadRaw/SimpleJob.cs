using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadRaw
{
    internal class SimpleJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Task.Run(() => Run());
        }

        public void Run()
        {
            Console.WriteLine("Logging at :- " + DateTime.Now);
        }
    }
}