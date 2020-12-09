using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Models;
using AppContext = DataAccess.AppContext;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        AppContext db = new DataAccess.AppContext();
       
        private readonly ILogger<HomeController> _logger;
                  
        public HomeController(ILogger<HomeController> logger )
        {
            _logger = logger;           
        }

        public IActionResult Index()
        {
            return View(db.Products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
