using LiteDB;
using MaintenanceService.DataAccess.DataModels;
using Microsoft.AspNetCore.Http;

namespace MaintenanceService.DataAccess.DataServises
{
    public class ApplicationService
    {
        private string _dbLocation = @$"{new DirectoryInfo(@"..\").FullName}\LiteDB\applog.db";

        public IEnumerable<ApplicationSystemModel> GetApplicationData()
        {
            using (var db = new LiteDatabase(_dbLocation))
            {
                var col = db.GetCollection<ApplicationSystemModel>("application");

                col.EnsureIndex(x => x.InsertDate);

                var results = col.FindAll().ToList();

                return results;
            }
        }
    }
}
