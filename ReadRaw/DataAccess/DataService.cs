using LiteDB;
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

                col.Insert(performanceData);

                col.EnsureIndex(x => x.InsertDate);
            }
        }

        public static void InsertApplicationData()
        {
            using (LiteDatabase db = new LiteDatabase(@$"{new DirectoryInfo(@"..\..\..\..\").FullName}\LiteDB\applog.db"))
            {
                ILiteCollection<ApplicationSystemModel> col = db.GetCollection<ApplicationSystemModel>("application");

                List<ApplicationSystemModel> performanceData = ApplicationSystemFiller.GetApplicationSystems();

                col.Insert(performanceData);

                col.EnsureIndex(x => x.InsertDate);
            }
        }
    }
}
