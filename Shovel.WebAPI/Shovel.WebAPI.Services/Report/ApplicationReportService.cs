using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Shovel.WebAPI.Models;
using Shovel.WebAPI.Services.Data.Interfaces;
using Shovel.WebAPI.Services.Report.Interfaces;

namespace Shovel.WebAPI.Services.Report
{
    public class ApplicationReportService : IReportService
    {
        private readonly IApplicationSystemDataService _applicationSystemDataService;

        public string WorkSheetName => "Запущенные приложения";

        public string ReportName => $"Report Application {DateTime.Now.ToString("f")}.xlsx"; 

        public ApplicationReportService(IApplicationSystemDataService applicationSystemDataService) 
        {
            this._applicationSystemDataService = applicationSystemDataService;
        }

        public async Task<FileResult> GetReport()
        {

            Dictionary<string, string> reportHeaders = new Dictionary<string, string>()
            {
                { "A", "Время запуска" },
                { "B", "Имя компьютера, на котором запущено приоложение" },
                { "C", "Название процесса" },
                { "D", "Количество запущенных потоков, для работы приложения" },
                { "E", "Заголовок приложения" },
                { "F", "Дата запущенной синзронизации" },
                { "G", "Был ли завершен данный процесс" },
                { "H", "Имя сервера" },
            };

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add(WorkSheetName);

                worksheet.Rows(1,1).Style.Font.SetBold(true);

                worksheet.Column(1).Width = 15;
                worksheet.Column(2).Width = 30;
                worksheet.Column(3).Width = 25;
                worksheet.Column(4).Width = 25;
                worksheet.Column(5).Width = 35;
                worksheet.Column(6).Width = 15;
                worksheet.Column(7).Width = 15;
                worksheet.Column(8).Width = 20;

                worksheet.Style.Font.SetFontName("Times New Roman");
                worksheet.Style.Alignment.SetWrapText(true);
                worksheet.Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                worksheet.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                int row = 1;
                foreach (var header in reportHeaders)
                {
                    worksheet.Cell($"{header.Key}{row}").Value = header.Value;
                }

                var data = await _applicationSystemDataService.GetApplicationSystems();
                foreach (var item in data)
                {
                    row++;

                    worksheet.Cell($"A{row}").Value = item.Starttime.HasValue 
                        ? item.Starttime.Value.ToString("g") : string.Empty;

                    worksheet.Cell($"B{row}").Value = item.Machinename;

                    worksheet.Cell($"C{row}").Value = item.Processname;

                    worksheet.Cell($"D{row}").Value = item.Threadscount;

                    worksheet.Cell($"E{row}").Value = item.Mainwindowtitle;

                    worksheet.Cell($"F{row}").Value = item.Synctime.HasValue
                        ? item.Synctime.Value.ToString("g") : string.Empty;

                    worksheet.Cell($"G{row}").Value = item.Hasexited.HasValue 
                        ? (item.Hasexited.Value ? "Да" : "Нет") : "Нет";

                    worksheet.Cell($"H{row}").Value = item.Server?.Baseaddress ?? string.Empty;

                }

                worksheet.Name = WorkSheetName;
                workbook.SaveAs(ReportName);


                var bytes = System.IO.File.ReadAllBytes(ReportName);

                System.IO.File.Delete(ReportName);

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
