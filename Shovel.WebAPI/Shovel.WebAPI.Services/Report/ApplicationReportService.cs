using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Shovel.WebAPI.Services.Data.Interfaces;
using Shovel.WebAPI.Services.Report.Interfaces;

namespace Shovel.WebAPI.Services.Report
{
    public class ApplicationReportService : IReportService
    {
        private readonly IApplicationSystemDataService _applicationSystemDataService;
        public string ReportName => $"Report Application {DateTime.Now.ToString("f")}.xlsx"; 

        public ApplicationReportService(IApplicationSystemDataService applicationSystemDataService) 
        {
            this._applicationSystemDataService = applicationSystemDataService;
        }

        public FileResult GetReport()
        {
            using (var workbook = new XLWorkbook())
            {
                var data = _applicationSystemDataService.GetApplicationSystems();
                var worksheet = workbook.Worksheets.Add("Sample Sheet");
                worksheet.Cell("A1").Value = "Hello World!";
                worksheet.Cell("A2").FormulaA1 = "=MID(A1, 7, 5)";
                worksheet.Name = "Hello World!";
                workbook.SaveAs(ReportName);


                var bytes = File.ReadAllBytes(ReportName);

                File.Delete(ReportName);

                const string contentType = IReportService.ResponseContentType;

                var fileContentResult = new FileContentResult(bytes, contentType)
                {
                    FileDownloadName = ReportName
                };

                return fileContentResult;
            }
        }
    }
}
