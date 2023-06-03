using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Shovel.WebAPI.Models;
using Shovel.WebAPI.Services.Data;
using Shovel.WebAPI.Services.Data.Interfaces;
using Shovel.WebAPI.Services.Report.Interfaces;

namespace Shovel.WebAPI.Services.Report
{
    public class PerformanceReportService : IReportService
    {
        private readonly IPerformanceSystemDataService _performanceSystemDataService;

        public string WorkSheetName => "Производительность устройств";

        public string ReportName => $"Report Performance {DateTime.Now.ToString("f")}.xlsx"; 

        public PerformanceReportService(IPerformanceSystemDataService performanceSystemDataService) 
        {
            this._performanceSystemDataService = performanceSystemDataService;
        }

        public async Task<FileResult> GetReport()
        {

            Dictionary<string, string> reportHeaders = new Dictionary<string, string>()
            {
                { "A", "Операционная система устройства" },
                { "B", "Уровень процессора" },
                { "C", "Имя пользователя системы" },
                { "D", "Количество байтов на странице памяти операционной системы" },
                { "E", "Является ли 64-х битовой ОС" },
                { "F", "Дата сохранения данных" },
                { "G", "Системная директория устройства" },
                { "H", "Имя устройства" },
                { "I", "Сколько устройство работает с момента включения, минут" },
                { "J", "Дата запущенной синхронизации" },
                { "K", "Архитектура процессора" },
                { "L", "Количество ядер процессора" },
                { "M", "Размер доступной оперативной памяти" },
                { "N", "Имя сетевого домена, связанное с текущим пользователем" },
                { "O", "Путь процесса" },
                { "P", "Имя сервера" }
            };

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add(WorkSheetName);

                #region Set List style

                worksheet.Rows(1,1).Style.Font.SetBold(true);

                worksheet.Column(1).Width = 15;
                worksheet.Column(2).Width = 30;
                worksheet.Column(3).Width = 25;
                worksheet.Column(4).Width = 25;
                worksheet.Column(5).Width = 35;
                worksheet.Column(6).Width = 15;
                worksheet.Column(7).Width = 30;
                worksheet.Column(8).Width = 30;
                worksheet.Column(9).Width = 20;
                worksheet.Column(10).Width = 20;
                worksheet.Column(11).Width = 20;
                worksheet.Column(12).Width = 20;
                worksheet.Column(13).Width = 20;
                worksheet.Column(14).Width = 20;
                worksheet.Column(15).Width = 50;
                worksheet.Column(16).Width = 30;

                worksheet.Style.Font.SetFontName("Times New Roman");
                worksheet.Style.Alignment.SetWrapText(true);
                worksheet.Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                worksheet.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                #endregion

                int row = 1;
                foreach (var header in reportHeaders)
                {
                    worksheet.Cell($"{header.Key}{row}").Value = header.Value;
                }

                var data = await _performanceSystemDataService.GetPerformanceSystems();
                foreach (var item in data)
                {
                    row++;

                    worksheet.Cell($"A{row}").Value = item.Operationsystem;

                    worksheet.Cell($"B{row}").Value = item.Processorlevel;

                    worksheet.Cell($"C{row}").Value = item.Username;

                    worksheet.Cell($"D{row}").Value = item.Systempagesize;

                    worksheet.Cell($"E{row}").Value = item.Is64bitoperatingsystem.HasValue
                        ? (item.Is64bitoperatingsystem.Value ? "Да" : "Нет") : "Нет";

                    worksheet.Cell($"F{row}").Value = item.Insertdate.HasValue
                        ? item.Insertdate.Value.ToString("g") : string.Empty;

                    worksheet.Cell($"G{row}").Value = item.Systemdirectory;

                    worksheet.Cell($"H{row}").Value = item.Machinename;

                    worksheet.Cell($"I{row}").Value = item.Tickcount64 / 60_000;

                    worksheet.Cell($"J{row}").Value = item.Synctime.HasValue
                        ? item.Synctime.Value.ToString("g") : string.Empty;

                    worksheet.Cell($"K{row}").Value = item.Processorarchitecture;

                    worksheet.Cell($"L{row}").Value = item.Processorcount;

                    worksheet.Cell($"M{row}").Value = item.Ramavailable;

                    worksheet.Cell($"N{row}").Value = item.Userdomainname;

                    worksheet.Cell($"O{row}").Value = item.Processpath;

                    worksheet.Cell($"P{row}").Value = item.Server?.Baseaddress ?? string.Empty;

                }

                worksheet.Name = WorkSheetName;
                workbook.SaveAs(ReportName);


                var bytes = System.IO.File.ReadAllBytes(ReportName);

                const string contentType = IReportService.ResponseContentType;

                var fileContentResult = new FileContentResult(bytes, contentType)
                {
                    FileDownloadName = ReportName
                };

                System.IO.File.Delete(ReportName);

                return fileContentResult;
            }
        }
    }
}
