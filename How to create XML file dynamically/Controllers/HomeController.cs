using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using How_to_create_XML_file_dynamically.Models;
using System.Data.SqlClient;
using System.Data;

namespace How_to_create_XML_file_dynamically.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GenerateXMLFile()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GenerateXMLFile(string action)
        {
            if (!string.IsNullOrEmpty(action))
            {
                string xmlData = generateFile();
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        

        SqlConnection connetDb()
        {
            string connetionString = @"Data Source=.;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection sqlConnection = new SqlConnection(connetionString);
            return sqlConnection;
        }

        DataSet getData()
        {
            var sqlconnection = this.connetDb();
            string command = "SELECT [OrderDate] ,[Region] ,[Rep] ,[Item] ,[Units] ,[UnitCost] ,[Total]  FROM [salesOrder] order by [OrderDate]";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command, sqlconnection);
            DataSet dataSet = new DataSet("data");
            sqlDataAdapter.Fill(dataSet);
            return dataSet;
        }

       private string generateFile()
        {
            var xmlData = this.getData().GetXml();
            return xmlData;
        }
    }
}
