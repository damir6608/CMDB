using Shovel.WebAPI.Services.Synchronize.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shovel.WebAPI.Services.Synchronize
{
    public class SynchronizationService : ISynchronizationService
    {
        private readonly IPerformanceSystemSynchronizeService _performanceSystemSynchronizeService;
        private readonly IApplicationSystemSynchronizeService _applicationSystemSynchronizeService;
        public SynchronizationService(IPerformanceSystemSynchronizeService performanceSystemSynchronizeService, IApplicationSystemSynchronizeService applicationSystemSynchronizeService)
        {
            _performanceSystemSynchronizeService = performanceSystemSynchronizeService;
            _applicationSystemSynchronizeService = applicationSystemSynchronizeService;
        }

        public async Task Execute()
        {
            _performanceSystemSynchronizeService.GetData();
            _applicationSystemSynchronizeService.GetData();
        }
    }
}
