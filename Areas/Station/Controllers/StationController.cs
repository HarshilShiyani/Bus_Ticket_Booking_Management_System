using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;
using System.Drawing.Printing;


namespace Bus_Ticket_Booking_Management_System.Areas.Station.Controllers
{
    [Area("Station")]
    [Route("Station/[controller]/[action]")]
    public class StationController : Controller
    {
        public static string ConnString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build()
            .GetConnectionString("connectionString");
        public ActionResult Index(int page = 1)
        {
            int totalRows = GetTotalRowCount(); // Implement this method to get the total number of rows
            int totalPages = (int)Math.Ceiling((double)totalRows / 7);

            DataTable dataForPage = GetDataForPage(page);

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View("StationList",dataForPage);
        }
        private DataTable GetDataForPage(int page)
        {
            SqlDatabase sqlDatabase = new SqlDatabase(ConnString);
            DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_SelectAll_Station");
            sqlDatabase.AddInParameter(dbCommand, "@PageNumber", DbType.Int32, page);
            sqlDatabase.AddInParameter(dbCommand, "@PageSize", DbType.Int32, 7);

            using (IDataReader reader = sqlDatabase.ExecuteReader(dbCommand))
            {
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                return dataTable;
            }
        }
        private int GetTotalRowCount()
        {
            SqlDatabase sqlDatabase = new SqlDatabase(ConnString);
            DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Count_Station");
            return Convert.ToInt32(sqlDatabase.ExecuteScalar(dbCommand));
        }
    }
}


