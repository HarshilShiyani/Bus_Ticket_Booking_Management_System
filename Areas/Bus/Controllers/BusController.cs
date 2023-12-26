using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;


namespace Bus_Ticket_Booking_Management_System.Areas.Bus.Controllers
{
    [Area("Bus")]
    [Route("Bus/[controller]/[action]")]
    public class BusController : Controller
    {
        private readonly IConfiguration _configuration;
        public BusController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BusTypeList()
        {
            string connection = this._configuration.GetConnectionString("connectionString");
            SqlConnection sqlConnection = new SqlConnection(connection);

            sqlConnection.Open();

            SqlCommand command = sqlConnection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[pr_selectall_bus_category_list]";
            SqlDataReader reader = command.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            sqlConnection.Close();
            return View("BusTypeList", dt);
        }

        public IActionResult BusList()
        {
            string connection = this._configuration.GetConnectionString("connectionString");
            SqlConnection sqlConnection = new SqlConnection(connection);

            sqlConnection.Open();

            SqlCommand command = sqlConnection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[PR_SelectAllBuses]";
            SqlDataReader reader = command.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            sqlConnection.Close();
            return View("BusList", dt);
        }

    }
}
