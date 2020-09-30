using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nexus_Service_Marketing_System.Connection;
using Nexus_Service_Marketing_System.Models;

namespace Nexus_Service_Marketing_System.Controllers
{
    public class RetailShowroomsController : Controller
    {
        private ModelsContext db = new ModelsContext();

        [Route("Admin/RetailShowrooms")]
        public IActionResult Index(string txtRetailShowroomName)
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin"))
            {
                List<retailShowrooms> results = db.retailShowrooms.ToList();

                if (results != null)
                {
                    if (txtRetailShowroomName != null)
                    {
                        results = results.Where(item => item.RetailShowroom_name.ToLower().Contains(txtRetailShowroomName.Trim().ToLower())).ToList();

                        if (results.Count == 0)
                        {
                            ViewBag.Msg = "No results found!";
                        }
                        else
                        {
                            ViewBag.Msg = results.Count + " result(s) found!";
                        }
                    }

                    return View(@"~/Views/Admin/retailShowrooms/Index.cshtml", results);
                }

                return View(@"~/Views/Admin/ErrorNoDataFound.cshtml");
            }

            return View(@"~/Views/Admin/ErrorPemission.cshtml");
        }

        [Route("Admin/RetailShowrooms/Details/{id}")]
        public IActionResult Details(int id)
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin"))
            {
                var result = db.retailShowrooms.Find(id);

                if (result != null)
                {
                    return View(@"~/Views/Admin/retailShowrooms/Details.cshtml", result);
                }

                return View(@"~/Views/Admin/retailShowrooms/Index.cshtml");
            }

            return View(@"~/Views/Admin/ErrorPemission.cshtml");
        }

        [HttpGet]
        [Route("Admin/RetailShowrooms/Create")]
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin"))
            {
                return View(@"~/Views/Admin/retailShowrooms/Create.cshtml");
            }

            return View(@"~/Views/Admin/ErrorPemission.cshtml");
        }

        [HttpPost]
        [Route("Admin/RetailShowrooms/Create")]
        public IActionResult Create(retailShowrooms retailShowroom)
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin"))
            {
                try
                {
                    var name = db.retailShowrooms.FirstOrDefault(item => item.RetailShowroom_name.Equals(retailShowroom.RetailShowroom_name));
                    var address = db.retailShowrooms.FirstOrDefault(item => item.RetailShowroom_address.Equals(retailShowroom.RetailShowroom_address));
                    var city = db.cities.FirstOrDefault(item => item.City_id == retailShowroom.RetailShowroom_city_id);                   

                    if (name == null && address == null && city != null)
                    {
                        if (ModelState.IsValid)
                        {
                            db.retailShowrooms.Add(retailShowroom);
                            db.SaveChanges();

                            return RedirectToAction("Index", "retailShowrooms");
                        }
                        else
                        {
                            ViewBag.Msg = "Model State is invalid!";
                        }
                    }

                    if (name != null)
                    {
                        ViewBag.Msg = "This retail showroom name has already existed!";
                    }

                    if (address != null)
                    {
                        ViewBag.Msg = "This retail showroom address has already existed!";
                    }

                    if (city == null)
                    {
                        ViewBag.Msg = "This city id does not exist!";
                    }

                    return View(@"~/Views/Admin/retailShowrooms/Create.cshtml");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error: ", ex.Message);
                }                
            }

            return View(@"~/Views/Admin/ErrorPemission.cshtml");
        }

        [HttpGet]
        [Route("Admin/RetailShowrooms/Update/{id}")]
        public IActionResult Update(int id)
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin"))
            {
                var result = db.retailShowrooms.Find(id);

                if (result != null)
                {
                    return View(@"~/Views/Admin/retailShowrooms/Update.cshtml", result);
                }

                return RedirectToAction("Index", "RetailShowrooms");
            }

            return View(@"~/Views/Admin/ErrorPemission.cshtml");
        }

        [HttpPost]        
        public IActionResult Update(retailShowrooms retailShowroom)
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin"))
            {
                try
                {
                    var result = db.retailShowrooms.Find(retailShowroom.RetailShowroom_id);
                    var name = db.retailShowrooms.FirstOrDefault(item => item.RetailShowroom_name.Equals(retailShowroom.RetailShowroom_name));
                    var address = db.retailShowrooms.FirstOrDefault(item => item.RetailShowroom_address.Equals(retailShowroom.RetailShowroom_address));
                    var city = db.cities.FirstOrDefault(item => item.City_id == retailShowroom.RetailShowroom_city_id);

                    if (name != null && retailShowroom.RetailShowroom_name != result.RetailShowroom_name)
                    {
                        ViewBag.Msg = "This retail showroom name has already existed!";

                        return View(@"~/Views/Admin/retailShowrooms/Update.cshtml", result);
                    }

                    if (address != null && retailShowroom.RetailShowroom_address != result.RetailShowroom_address)
                    {
                        ViewBag.Msg = "This retail showroom address has already existed!";

                        return View(@"~/Views/Admin/retailShowrooms/Update.cshtml", result);
                    }

                    if (city == null)
                    {
                        ViewBag.Msg = "This city id does not exist!";

                        return View(@"~/Views/Admin/retailShowrooms/Update.cshtml", result);
                    }

                    if (ModelState.IsValid)
                    {
                        result.RetailShowroom_city_id = retailShowroom.RetailShowroom_city_id;
                        result.RetailShowroom_name = retailShowroom.RetailShowroom_name;
                        result.RetailShowroom_address = retailShowroom.RetailShowroom_address;
                        result.RetailShowroom_status = retailShowroom.RetailShowroom_status;

                        db.SaveChanges();

                        return RedirectToAction("Index", "RetailShowrooms");
                    }
                    else
                    {
                        ViewBag.Msg = "Model State is invalid!";
                    }

                    return View(@"~/Views/Admin/retailShowrooms/Update.cshtml", result);
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
