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
    public class PlansOrdersController : Controller
    {
        private ModelsContext db = new ModelsContext();

        [Route("Admin/PlansOrders")]
        public IActionResult Index(string txtPlansOrdersId)
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin") || HttpContext.Session.GetString("accRole").Equals("Accountant") || HttpContext.Session.GetString("accRole").Equals("Technician"))
            {
                List<plans_orders> results = db.plans_orders.Where(item => item.Plans_orders_id.Substring(0, 1).Equals("T")).ToList();

                if (results != null)
                {
                    if (txtPlansOrdersId != null)
                    {
                        results = results.Where(item => item.Plans_orders_id.ToLower().Contains(txtPlansOrdersId.Trim().ToLower())).ToList();

                        if (results.Count == 0)
                        {
                            ViewBag.Msg = "No results found!";
                        }
                        else
                        {
                            ViewBag.Msg = results.Count + " result(s) found!";
                        }
                    }

                    return View(@"~/Views/Admin/plans-orders/Index.cshtml", results);
                }

                return View(@"~/Views/Admin/ErrorNoDataFound.cshtml");
            }

            return View(@"~/Views/Admin/ErrorPemission.cshtml");
        }

        [Route("Admin/PlansOrders/Details/{id}")]
        public IActionResult Details(string id, DateTime txtPeriod)
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin") || HttpContext.Session.GetString("accRole").Equals("Accountant") || HttpContext.Session.GetString("accRole").Equals("Technician") || HttpContext.Session.GetString("accRole").Equals("SalesPerson"))
            {
                var results = db.tracks.Where(item => item.Track_plans_orders_id.Equals(id)).ToList();
                ViewBag.Status = db.plans_orders.Find(id).Plans_orders_status;

                if (results != null)
                {
                    if (txtPeriod != DateTime.MinValue)
                    {
                        var temp = txtPeriod.AddMonths(1);
                        var dateTo = temp.AddDays(-(temp.Day)).ToString("MM/dd/yyyy");

                        results = results.Where(item => item.Track_date_to.Equals(dateTo)).ToList();

                        if (results.Count == 0)
                        {
                            ViewBag.Msg = "No results found!";
                        }
                        else
                        {
                            ViewBag.Msg = results.Count + " result(s) found!";
                        }
                    }

                    return View(@"~/Views/Admin/plans-orders/Details.cshtml", results);
                }

                return View(@"~/Views/Admin/plans-orders/Index.cshtml");
            }

            return View(@"~/Views/Admin/ErrorPemission.cshtml");
        }

        [HttpGet]
        [Route("Admin/PlansOrders/Update/{id}")]
        public IActionResult Update(string id)
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin") || HttpContext.Session.GetString("accRole").Equals("Accountant") || HttpContext.Session.GetString("accRole").Equals("Technician"))
            {
                var result = db.plans_orders.Find(id);
                
                if (result != null)
                {
                    return View(@"~/Views/Admin/plans-orders/Update.cshtml", result);
                }

                return RedirectToAction("Index", "Plans");
            }

            return View(@"~/Views/Admin/ErrorPemission.cshtml");
        }

        [HttpPost]
        public IActionResult Update(plans_orders item)
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin") || HttpContext.Session.GetString("accRole").Equals("Accountant") || HttpContext.Session.GetString("accRole").Equals("Technician"))
            {
                try
                {
                    var result = db.plans_orders.Find(item.Plans_orders_id);

                    if (ModelState.IsValid)
                    {
                        result.Plans_orders_status = item.Plans_orders_status;

                        db.SaveChanges();

                        return RedirectToAction("Index", "PlansOrders");
                    }
                    else
                    {
                        ViewBag.Msg = "Model State is invalid!";
                    }

                    return View(@"~/Views/Admin/plans-orders/Update.cshtml", result);
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