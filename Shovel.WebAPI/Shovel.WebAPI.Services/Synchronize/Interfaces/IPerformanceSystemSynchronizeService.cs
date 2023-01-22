﻿using Shovel.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shovel.WebAPI.Services.Synchronize.Interfaces
{
    public interface IPerformanceSystemSynchronizeService
    {
        public Task<List<PerformanceSystem>> GetData();
    }
}
