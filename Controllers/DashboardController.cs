using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Nexus_Service_Marketing_System.Controllers
{
    public class DashboardController : Controller
    {
        [Route("Admin/Dashboard")]
        public IActionResult Index()
        {
            return View(@"~/Views/Admin/Dashboard.cshtml");
        }
    }
}