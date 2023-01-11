using Quartz.Impl;
using Quartz;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;

namespace ReadRaw
{
    internal class Scheduler
    {
        private readonly IScheduler _scheduler;
        public Scheduler()
        {
            NameValueCollection props = new NameValueCollection
        {
            { "quartz.serializer.type", "binary" },
            { "quartz.scheduler.instanceName", "Ncu.Schedulers.Activities" },
            { "quartz.jobStore.type", "Quartz.Simpl.RAMJobStore, Quartz" },
            { "quartz.threadPool.threadCount", "3" }
        };
            StdSchedulerFactory factory = new StdSchedulerFactory(props);
            _scheduler = factory.GetScheduler().ConfigureAwait(false).GetAwaiter().GetResult();
        }
        public void Start()
        {
            _scheduler.Start().ConfigureAwait(false).GetAwaiter().GetResult();

            ScheduleJobs();
        }
        public void ScheduleJobs()
        {
#pragma warning disable CS8604 // Possible null reference argument.
            IJobDetail job = JobBuilder.Create<SimpleJob>()
                .WithIdentity(ConfigurationManager.AppSettings["JobName"], ConfigurationManager.AppSettings["GroupName"])
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity(ConfigurationManager.AppSettings["TriggerName"], ConfigurationManager.AppSettings["GroupName"])
                .StartNow()
                 .WithCronSchedule(ConfigurationManager.AppSettings["Schedule"])
                .Build();
#pragma warning restore CS8604 // Possible null reference argument.

            // Tell quartz to schedule the job using our trigger
            _scheduler.ScheduleJob(job, trigger).ConfigureAwait(false).GetAwaiter().GetResult();
        }
        public void Stop()
        {
            _scheduler.Shutdown().ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}
