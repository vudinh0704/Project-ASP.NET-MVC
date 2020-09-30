using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nexus_Service_Marketing_System.Models;
using Nexus_Service_Marketing_System.Connection;

namespace Nexus_Service_Marketing_System.Controllers
{
    public class PlansController : Controller
    {
        private ModelsContext db = new ModelsContext();

        [Route("Admin/Plans")]
        public IActionResult Index(string txtPlanName)
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin") || HttpContext.Session.GetString("accRole").Equals("Technician"))
            {
                TempData.Keep();

                List<plans> results = db.plans.ToList();

                if (results != null)
                {
                    if (txtPlanName != null)
                    {
                        results = results.Where(item => item.Plan_name.ToLower().Contains(txtPlanName.Trim().ToLower())).ToList();

                        if (results.Count == 0)
                        {
                            ViewBag.Msg = "No results found!";
                        }
                        else
                        {
                            ViewBag.Msg = results.Count + " result(s) found!";
                        }
                    }

                    return View(@"~/Views/Admin/plans/Index.cshtml", results);
                }

                return View(@"~/Views/Admin/ErrorNoDataFound.cshtml");
            }

            return View(@"~/Views/Admin/ErrorPemission.cshtml");
        }

        [Route("Admin/Plans/Details/{id}")]        
        public IActionResult Details(int id)
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin") || HttpContext.Session.GetString("accRole").Equals("Technician"))
            {
                var result = db.plans.Find(id);

                if (result != null)
                {
                    return View(@"~/Views/Admin/plans/Details.cshtml", result);
                }

                return View(@"~/Views/Admin/plans/Index.cshtml");
            }

            return View(@"~/Views/Admin/ErrorPemission.cshtml");
        }

        [HttpGet]
        [Route("Admin/Plans/Create")]
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin") || HttpContext.Session.GetString("accRole").Equals("Technician"))
            {
                return View(@"~/Views/Admin/plans/Create.cshtml");
            }

            return View(@"~/Views/Admin/ErrorPemission.cshtml");
        }

        [HttpPost]
        [Route("Admin/Plans/Create")]
        public IActionResult Create(plans plan, IFormFile txtFileName)
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin") || HttpContext.Session.GetString("accRole").Equals("Technician"))
            {
                try
                {                    
                    var connection = db.connections.FirstOrDefault(item => item.Connection_id == plan.Plan_connection_id);

                    if (connection == null)
                    {
                        ViewBag.Msg = "This connection id does not exist!";

                        return View(@"~/Views/Admin/plans/Create.cshtml");
                    }

                    if (txtFileName == null)
                    {
                        ViewBag.Msg = "You must select the product's image!";

                        return View(@"~/Views/Admin/plans/Create.cshtml");
                    }

                    plan.Plan_image_thumbnail = txtFileName.FileName;

                    if (ModelState.IsValid)
                    {
                        db.plans.Add(plan);
                        db.SaveChanges();

                        return RedirectToAction("Index", "Plans");
                    }
                    else
                    {
                        ViewBag.Msg = "Model State is invalid!";
                    }

                    return View(@"~/Views/Admin/plans/Create.cshtml");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error: ", ex.Message);
                }
            }

            return View(@"~/Views/Admin/ErrorPemission.cshtml");
        }
        
        [HttpGet]
        [Route("Admin/Plans/Update/{id}")]
        public IActionResult Update(int id)
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin") || HttpContext.Session.GetString("accRole").Equals("Technician"))
            {
                var result = db.plans.Find(id);

                if (result != null)
                {
                    return View(@"~/Views/Admin/plans/Update.cshtml", result);
                }

                return RedirectToAction("Index", "Plans");
            }

            return View(@"~/Views/Admin/ErrorPemission.cshtml");
        }

        [HttpPost]        
        public IActionResult Update(plans plan, IFormFile txtFileName)
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin") || HttpContext.Session.GetString("accRole").Equals("Technician"))
            {
                try
                {
                    var result = db.plans.Find(plan.Plan_id);
                    var connection = db.connections.FirstOrDefault(item => item.Connection_id == plan.Plan_connection_id);

                    if (connection == null)
                    {
                        ViewBag.Msg = "This connection id does not exist!";

                        return View(@"~/Views/Admin/plans/Update.cshtml", result);
                    }
                    
                    if (txtFileName == null)
                    {
                        ViewBag.Msg = "You must select the product's image!";

                        return View(@"~/Views/Admin/plans/Update.cshtml", result);
                    }

                    if (ModelState.IsValid)
                    {
                        result.Plan_connection_id = plan.Plan_connection_id;
                        result.Plan_name = plan.Plan_name;
                        result.Plan_description = plan.Plan_description;                        
                        result.Plan_fixed_price = plan.Plan_fixed_price;
                        result.Plan_local_price = plan.Plan_local_price;
                        result.Plan_std_price = plan.Plan_std_price;
                        result.Plan_messaging_for_mobiles_price = plan.Plan_messaging_for_mobiles_price;                        
                        result.Plan_image_thumbnail = txtFileName.FileName;

                        db.SaveChanges();

                        return RedirectToAction("Index", "Plans");
                    }
                    else
                    {
                        ViewBag.Msg = "Model State is invalid!";
                    }

                    return View(@"~/Views/Admin/plans/Update.cshtml", result);
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
