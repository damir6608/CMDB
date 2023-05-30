using Microsoft.AspNetCore.Mvc;

namespace Shovel.WebAPI.Services.Report.Interfaces
{
    public interface IReportService
    {
        public const string ResponseContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        public string WorkSheetName { get; }

        public string ReportName { get; }

        public Task<FileResult> GetReport();
    }
}
