using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shovel.WebAPI.Services.Report.Interfaces
{
    public interface IReportService
    {
        public const string ResponseContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        public string ReportName { get; }
        public FileResult GetReport();
    }
}
