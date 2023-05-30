using LiteDB;
using MaintenanceService.DataAccess.DataModels;
using Microsoft.AspNetCore.Http;

namespace MaintenanceService.DataAccess.DataServises
{
    public class PerformanceService
    {
        private string _dbLocation = @$"{new DirectoryInfo(@"..\").FullName}\LiteDB\applog.db";

        public IEnumerable<PerformanceModel> GetPerformanceData(DateTime lastSyncDate)
        {
            using (var db = new LiteDatabase(_dbLocation))
            {
                ILiteCollection<PerformanceModel> col = db.GetCollection<PerformanceModel>("performance");

                col.EnsureIndex(x => x.InsertDate);

                List<PerformanceModel> results = col.FindAll().Where(i => i.InsertDate > lastSyncDate).ToList();

                return results;
            }
        }
    }
}
