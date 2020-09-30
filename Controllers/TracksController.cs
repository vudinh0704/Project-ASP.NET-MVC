using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nexus_Service_Marketing_System.Models;
using Nexus_Service_Marketing_System.Connection;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Nexus_Service_Marketing_System.Controllers
{
    public class TracksController : Controller
    {
        private ModelsContext db = new ModelsContext();

        [Route("Admin/Tracks")]
        public IActionResult Index(string txtTrackId)
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin") || HttpContext.Session.GetString("accRole").Equals("Accountant") || HttpContext.Session.GetString("accRole").Equals("Technician") || HttpContext.Session.GetString("accRole").Equals("SalesPerson"))
            {                
                List<tracks> results = db.tracks.ToList();

                if (results != null)
                {
                    if (txtTrackId != null)
                    {
                        results = results.Where(item => item.Track_id.ToLower().Contains(txtTrackId.Trim().ToLower())).ToList();

                        if (results.Count == 0)
                        {
                            ViewBag.Msg = "No results found!";
                        }
                        else
                        {
                            ViewBag.Msg = results.Count + " result(s) found!";
                        }
                    }

                    return View(@"~/Views/Admin/tracks/Index.cshtml", results);
                }

                return View(@"~/Views/Admin/ErrorNoDataFound.cshtml");
            }

            return View(@"~/Views/Admin/ErrorPemission.cshtml");
        }

        [HttpGet]
        [Route("Admin/Tracks/Create")]
        public IActionResult Create(string txtPlansOrdersId)
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin") || HttpContext.Session.GetString("accRole").Equals("Accountant") || HttpContext.Session.GetString("accRole").Equals("Technician"))
            {                
                if (txtPlansOrdersId != null)
                {
                    var plansOrders = db.plans_orders.Find(txtPlansOrdersId);
                    var order = db.orders.Find(plansOrders.Plans_orders_order_id);
                    var orderStatus = order.Order_status;
                    var temp = db.plans_orders.Find(txtPlansOrdersId);

                    if (!orderStatus.Equals("Complete") || (temp.Plans_orders_plan_price_local + temp.Plans_orders_plan_price_std + temp.Plans_orders_plan_price_msg) == 0)
                    {
                        return RedirectToAction("Index", "PlansOrders");
                    }

                    ViewBag.PlansOrdersId = txtPlansOrdersId;
                }

                return View(@"~/Views/Admin/tracks/Create.cshtml");
            }

            return View(@"~/Views/Admin/ErrorPemission.cshtml");
        }

        [HttpPost]
        [Route("Admin/Tracks/Create")]
        public IActionResult Create(tracks track, DateTime txtPeriod)
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin") || HttpContext.Session.GetString("accRole").Equals("Accountant") || HttpContext.Session.GetString("accRole").Equals("Technician"))
            {
                try
                {
                    var temp = txtPeriod.AddMonths(1);
                    var month = txtPeriod.Month;
                    var year = txtPeriod.Year;
                    var dateFrom = new DateTime(year, month, 1).ToString("MM/dd/yyyy");
                    var dateTo = temp.AddDays(-(temp.Day)).ToString("MM/dd/yyyy");

                    var check = db.plans_orders.FirstOrDefault(item => item.Plans_orders_id.Equals(track.Track_plans_orders_id));
                    var results = db.tracks.Where(item => item.Track_plans_orders_id.Equals(track.Track_plans_orders_id)).ToList();
                    var result = results.FirstOrDefault(item => item.Track_date_from.Equals(dateFrom));

                    track.Track_id = track.Track_plans_orders_id + "-" + txtPeriod.ToString("MM-yyyy");
                    track.Track_date_from = dateFrom;
                    track.Track_date_to = dateTo;
                    track.Track_status = "Pending";

                    if (check == null)
                    {                        
                        ViewBag.Msg = "Invalid Plans Orders ID!";

                        return View(@"~/Views/Admin/tracks/Create.cshtml");
                    }

                    if (result != null)
                    {
                        ViewBag.PlansOrdersId = track.Track_plans_orders_id;
                        ViewBag.Msg = "This track period has already existed!";

                        return View(@"~/Views/Admin/tracks/Create.cshtml");
                    }

                    if (txtPeriod == DateTime.MinValue)
                    {
                        ViewBag.PlansOrdersId = track.Track_plans_orders_id;
                        ViewBag.Msg = "You must choose a track period!";

                        return View(@"~/Views/Admin/tracks/Create.cshtml");
                    }

                    db.tracks.Add(track);
                    db.SaveChanges();

                    return RedirectToAction("Index", "Tracks");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error: ", ex.Message);
                }

                return View(@"~/Views/Admin/tracks/Create.cshtml");
            }

            return View(@"~/Views/Admin/ErrorPemission.cshtml");
        }

        [HttpGet]
        [Route("Admin/Tracks/Update/{id}")]
        public IActionResult Update(string id)
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin") || HttpContext.Session.GetString("accRole").Equals("Accountant") || HttpContext.Session.GetString("accRole").Equals("Technician"))
            {
                var result = db.tracks.Find(id);

                if (result != null)
                {
                    if (!result.Track_status.Equals("Complete"))
                    {
                        return View(@"~/Views/Admin/tracks/Update.cshtml", result);
                    }                    
                }

                return RedirectToAction("Index", "Tracks");
            }

            return View(@"~/Views/Admin/ErrorPemission.cshtml");
        }

        [HttpPost]
        public IActionResult Update(tracks track)
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin") || HttpContext.Session.GetString("accRole").Equals("Accountant") || HttpContext.Session.GetString("accRole").Equals("Technician"))
            {
                try
                {
                    var result = db.tracks.Find(track.Track_id);

                    result.Track_time_used_local = track.Track_time_used_local;
                    result.Track_time_used_std = track.Track_time_used_std;
                    result.Track_time_used_msg = track.Track_time_used_std;
                    result.Track_status = track.Track_status;

                    db.SaveChanges();

                    return RedirectToAction("Index", "Tracks");                    
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