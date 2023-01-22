using Microsoft.Extensions.Logging;
using Quartz;
using Shovel.WebAPI.Services.Synchronize.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shovel.WebAPI.QuartzJobsAndTriggers.Jobs
{
    internal class SynchronizationJob : IJob
    {
        private readonly ILogger<SynchronizationJob> _logger;
        private readonly ISynchronizationService _synchronizationService;
        public SynchronizationJob(ILogger<SynchronizationJob> logger, ISynchronizationService synchronizationService)
        {
            _logger = logger;
            _synchronizationService = synchronizationService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation($"Synchronization started at {DateTime.Now}");
            await _synchronizationService.Execute();
            _logger.LogInformation($"Synchronization ended at {DateTime.Now}");
        }
    }
}