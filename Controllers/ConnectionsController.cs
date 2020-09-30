using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nexus_Service_Marketing_System.Models;
using Nexus_Service_Marketing_System.Connection;
using Microsoft.EntityFrameworkCore;

namespace Nexus_Service_Marketing_System.Controllers
{
    public class ConnectionsController : Controller
    {
        private ModelsContext db = new ModelsContext();

        [Route("Admin/Connections")]
        public IActionResult Index(string txtConnectionName)
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin") || HttpContext.Session.GetString("accRole").Equals("Technician"))
            {
                TempData.Keep();

                List<connections> results = db.connections.ToList();

                if (results != null)
                {
                    if (txtConnectionName != null)
                    {
                        results = results.Where(item => item.Connection_name.ToLower().Contains(txtConnectionName.Trim().ToLower())).ToList();

                        if (results.Count == 0)
                        {
                            ViewBag.Msg = "No results found!";
                        }
                        else
                        {
                            ViewBag.Msg = results.Count + " result(s) found!";
                        }
                    }

                    return View(@"~/Views/Admin/connections/Index.cshtml", results);
                }

                return View(@"~/Views/Admin/ErrorNoDataFound.cshtml");
            }

            return View(@"~/Views/Admin/ErrorPemission.cshtml");
        }

        [Route("Admin/Connections/Details/{id}")]        
        public IActionResult Details(int id)
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin") || HttpContext.Session.GetString("accRole").Equals("Technician"))
            {                
                var result = db.connections.Find(id);

                if (result != null)
                {
                    return View(@"~/Views/Admin/connections/Details.cshtml", result);
                }

                return View(@"~/Views/Admin/connections/Index.cshtml");
            }

            return View(@"~/Views/Admin/ErrorPemission.cshtml");
        }

        [HttpGet]
        [Route("Admin/Connections/Create")]
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin") || HttpContext.Session.GetString("accRole").Equals("Technician"))
            {
                return View(@"~/Views/Admin/connections/Create.cshtml");
            }

            return View(@"~/Views/Admin/ErrorPemission.cshtml");
        }

        [HttpPost]
        [Route("Admin/Connections/Create")]
        public IActionResult Create(connections connection)
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin") || HttpContext.Session.GetString("accRole").Equals("Technician"))
            {
                try
                {
                    if (connection.Connection_image_thumbnail == null)
                    {
                        ViewBag.Msg = "You must select the connection's image!";
                    }
                    else if (ModelState.IsValid)
                    {
                        db.connections.Add(connection);
                        db.SaveChanges();

                        return RedirectToAction("Index", "Connections");
                    }
                    else
                    {
                        ViewBag.Msg = "Model State is invalid!";
                    }

                    return View(@"~/Views/Admin/connections/Create.cshtml");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error: ", ex.Message);
                }                
            }

            return View(@"~/Views/Admin/ErrorPemission.cshtml");
        }

        [HttpGet]
        [Route("Admin/Connections/Update/{id}")]
        public IActionResult Update(int id)
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin") || HttpContext.Session.GetString("accRole").Equals("Technician"))
            {
                var result = db.connections.Find(id);

                if (result != null)
                {
                    return View(@"~/Views/Admin/connections/Update.cshtml", result);
                }

                return RedirectToAction("Index", "Connections");
            }

            return View(@"~/Views/Admin/ErrorPemission.cshtml");
        }

        [HttpPost]        
        public IActionResult Update(connections connection)
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin") || HttpContext.Session.GetString("accRole").Equals("Technician"))
            {
                try
                {
                    var result = db.connections.Find(connection.Connection_id);

                    if (connection.Connection_image_thumbnail == null)
                    {
                        ViewBag.Msg = "You must select the connection's image!";
                    }
                    else if (ModelState.IsValid)
                    {
                        result.Connection_name = connection.Connection_name;
                        result.Connection_description = connection.Connection_description;
                        result.Connection_image_thumbnail = connection.Connection_image_thumbnail;                        

                        db.SaveChanges();

                        return RedirectToAction("Index", "Connections");
                    }
                    else
                    {
                        ViewBag.Msg = "Model State is invalid!";
                    }

                    return View(@"~/Views/Admin/connections/Update.cshtml", result);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error: ", ex.Message);
                }                
            }

            return View(@"~/Views/Admin/ErrorPemission.cshtml");
        }        
    }
}
