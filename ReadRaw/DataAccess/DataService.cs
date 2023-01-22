using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using System.Text.Json;
using ReadRaw.SystemData;
using ReadRaw.DataModels;

namespace ReadRaw.DataAccess
{
    internal class DataService
    {
        public static void InsertPerformanceData()
        {
            using (LiteDatabase db = new LiteDatabase(@$"{new DirectoryInfo(@"..\..\..\..\").FullName}\LiteDB\applog.db"))
            {
                ILiteCollection<PerformanceModel> col = db.GetCollection<PerformanceModel>("performance");

                PerformanceModel performanceData = PerformanceDataFiller.GetPerformanceCounter();

                // Insert new customer document (Id will be auto-incremented)
                col.Insert(performanceData);

                // Index document using document Name property
                col.EnsureIndex(x => x.InsertDate);
            }
        }
        public static void InsertApplicationData()
        {
            using (LiteDatabase db = new LiteDatabase(@$"{new DirectoryInfo(@"..\..\..\..\").FullName}\LiteDB\applog.db"))
            {
                ILiteCollection<ApplicationSystemModel> col = db.GetCollection<ApplicationSystemModel>("application");

                List<ApplicationSystemModel> performanceData = ApplicationSystemFiller.GetApplicationSystems();

                // Insert new customer document (Id will be auto-incremented)
                col.Insert(performanceData);

                // Index document using document Name property
                col.EnsureIndex(x => x.InsertDate);
            }
        }
    }
}
