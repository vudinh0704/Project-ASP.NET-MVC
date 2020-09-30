using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nexus_Service_Marketing_System.Models;
using Nexus_Service_Marketing_System.Connection;
using Microsoft.AspNetCore.Server.Kestrel.Core.Features;

namespace Nexus_Service_Marketing_System.Controllers
{
    public class BillsController : Controller
    {
        private ModelsContext db = new ModelsContext();

        [Route("Admin/Bills")]
        public IActionResult Index(string txtBillId)
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin") || HttpContext.Session.GetString("accRole").Equals("Accountant") || HttpContext.Session.GetString("accRole").Equals("SalesPerson"))
            {
                var results = db.bills.ToList();

                if (results != null)
                {
                    if (txtBillId != null)
                    {
                        results = results.Where(item => item.Bill_id.ToLower().Contains(txtBillId.Trim().ToLower())).ToList();

                        if (results.Count == 0)
                        {
                            ViewBag.Msg = "No results found!";
                        }
                        else
                        {
                            ViewBag.Msg = results.Count + " result(s) found!";
                        }
                    }

                    return View(@"~/Views/Admin/bills/Index.cshtml", results);
                }

                return View(@"~/Views/Admin/ErrorNoDataFound.cshtml");
            }

            return View(@"~/Views/Admin/ErrorPemission.cshtml");
        }

        [Route("Admin/Bills/Details/{id}")]
        public IActionResult Details(string id)
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin") || HttpContext.Session.GetString("accRole").Equals("Accountant") || HttpContext.Session.GetString("accRole").Equals("SalesPerson"))
            {
                var result = db.bills.Find(id);
                var orderEquipments = db.equipments_orders.Where(item => item.Equipments_orders_order_id.Equals(result.Bill_order_id)).ToList();
                var orderPlans = db.plans_orders.Where(item => item.Plans_orders_order_id.Equals(result.Bill_order_id)).ToList();
                var track = db.tracks.FirstOrDefault(item => item.Track_id.Equals(result.Bill_track_id));
                
                ViewBag.Count = 0;
                ViewBag.SubTotal = 0;
                ViewBag.Rate = "0%";
                ViewBag.VAT = 0;
                ViewBag.Discount = 0;
                ViewBag.Deposit = 0;

                if (orderEquipments == null && orderPlans == null && track == null)
                {
                    return RedirectToAction("Index", "Bills");
                }

                if (orderEquipments != null && orderPlans != null)
                {                                     
                    ViewBag.Equipments = orderEquipments;
                    ViewBag.Plans = orderPlans;

                    if (orderEquipments != null)
                    {
                        ViewBag.Count += orderEquipments.Sum(item => item.Equipments_orders_quantity);
                        ViewBag.SubTotal += orderEquipments.Sum(item => item.Equipments_orders_quantity * item.Equipments_orders_equipment_price);                        
                    }

                    if (orderPlans != null)
                    {                        
                        ViewBag.Count += orderPlans.Count();
                        ViewBag.SubTotal += orderPlans.Sum(item => item.Plans_orders_plan_price);
                        ViewBag.Deposit = orderPlans.Sum(item => item.Plans_orders_plan_security_deposit);

                        if (orderPlans.Count() > 10 && orderPlans.Count() <= 15)
                        {
                            ViewBag.Rate = "25%";
                            ViewBag.Discount = orderPlans.Sum(item => item.Plans_orders_plan_price) / 4;
                        }
                        else if (orderPlans.Count() > 15 && orderPlans.Count() <= 25)
                        {
                            ViewBag.Rate = "75%";
                            ViewBag.Discount = orderPlans.Sum(item => item.Plans_orders_plan_price) * 3 / 4; ;
                        }
                        else if (orderPlans.Count() > 25 && orderPlans.Count() <= 50)
                        {
                            ViewBag.Rate = "100%";
                            ViewBag.Discount = ViewBag.Discount = orderPlans.Sum(item => item.Plans_orders_plan_price);
                        }
                        else
                        {
                            ViewBag.Discount = 0;
                        }
                    }
                                    
                    ViewBag.VAT = ViewBag.SubTotal * 12.24 / 100;                    
                    ViewBag.Total = Convert.ToDouble(ViewBag.SubTotal) + Convert.ToDouble(ViewBag.VAT) + Convert.ToDouble(ViewBag.Deposit) - Convert.ToDouble(ViewBag.Discount);                    
                }

                if (track != null)
                {
                    ViewBag.Count = 0;
                    ViewBag.Total = 0;
                    ViewBag.Track = track;

                    ViewBag.Count += track.Track_time_used_local + track.Track_time_used_std + track.Track_time_used_msg;
                    ViewBag.SubTotal += track.Track_time_used_local * track.Track_plan_price_local + track.Track_time_used_std * track.Track_plan_price_std + track.Track_time_used_msg * track.Track_plan_price_msg;
                    ViewBag.VAT = ViewBag.SubTotal * 12.24 / 100;
                    ViewBag.Total = ViewBag.SubTotal + ViewBag.VAT;
                }

                return View(@"~/Views/Admin/bills/Details.cshtml");
            }

            return View(@"~/Views/Admin/ErrorPemission.cshtml");
        }

        [HttpGet]
        [Route("Admin/Bills/Create")]
        public IActionResult Create(string txtBasedOnId)
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin") || HttpContext.Session.GetString("accRole").Equals("Accountant"))
            {
                if (txtBasedOnId != null)
                {
                    var number = 0;
                    var order = db.orders.Find(txtBasedOnId);
                    
                    var track = db.tracks.Find(txtBasedOnId);
                    List<bills> results = db.bills.ToList();                    

                    if (results.Count > 0)
                    {
                        number = int.Parse(results[results.Count - 1].Bill_id) + 1;
                    }
                    else
                    {
                        number = 1;
                    }

                    ViewBag.BillId = String.Format("{0:0000000000}", number);

                    if (order != null)
                    {
                        var checkBill = db.bills.FirstOrDefault(item => item.Bill_order_id.Equals(txtBasedOnId));

                        if (checkBill != null)
                        {
                            return RedirectToAction("Index", "Orders");                            
                        }

                        ViewBag.OrderId = order.Order_id;
                    }

                    if (track != null)
                    {
                        var checkBill = db.bills.FirstOrDefault(item => item.Bill_track_id.Equals(txtBasedOnId));

                        if (checkBill != null)
                        {
                            return RedirectToAction("Index", "Tracks");                            
                        }

                        ViewBag.TrackId = track.Track_id;
                    }                    
                }

                return View(@"~/Views/Admin/Bills/Create.cshtml");
            }

            return View(@"~/Views/Admin/ErrorPemission.cshtml");
        }

        [HttpPost]
        [Route("Admin/Bills/Create")]
        public IActionResult Create(bills bill)
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin") || HttpContext.Session.GetString("accRole").Equals("Accountant"))
            {
                try
                {
                    if (bill.Bill_order_id != null)
                    {
                        var order = db.orders.Find(bill.Bill_order_id);
                        var orderStatus = order.Order_status;

                        if (!orderStatus.Equals("Complete"))
                        {
                            ViewBag.Msg = "You cannot invoice when the order is not completed";

                            return View(@"~/Views/Admin/Bills/Create.cshtml");
                        }
                        else
                        {
                            order.Order_status = "Billed";
                        }
                    }

                    if (bill.Bill_track_id != null)
                    {
                        var track = db.tracks.Find(bill.Bill_track_id);
                        var trackStatus = track.Track_status;

                        if (!trackStatus.Equals("Complete"))
                        {
                            ViewBag.Msg = "You cannot invoice when the track is not completed";

                            return View(@"~/Views/Admin/Bills/Create.cshtml");
                        }
                        else
                        {
                            track.Track_status = "Billed";
                        }
                    }

                    bill.Bill_status = "Unpaid";

                    db.bills.Add(bill);
                    db.SaveChanges();

                    return RedirectToAction("Index", "Bills");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error: ", ex.Message);
                }                
            }

            return View(@"~/Views/Admin/ErrorPemission.cshtml");
        }

        [HttpGet]
        [Route("Admin/Bills/Update/{id}")]
        public IActionResult Update(int id)
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin") || HttpContext.Session.GetString("accRole").Equals("Accountant"))
            {
                var result = db.bills.Find(id);

                if (result != null)
                {
                    return View(@"~/Views/Admin/Bills/Update.cshtml", result);
                }

                return RedirectToAction("Index", "Bills");
            }

            return View(@"~/Views/Admin/ErrorPemission.cshtml");
        }

        [HttpPost]
        public IActionResult Update(bills Bill)
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin") || HttpContext.Session.GetString("accRole").Equals("Accountant"))
            {
                try
                {
                    var result = db.bills.FirstOrDefault(item => item.Bill_id.Equals(Bill.Bill_id));

                    if (ModelState.IsValid)
                    {
                        result.Bill_status = Bill.Bill_status;

                        db.SaveChanges();

                        return RedirectToAction("Index", "Bills");
                    }
                    else
                    {
                        ViewBag.Msg = "Model State is invalid!";
                    }

                    return View(@"~/Views/Admin/Bills/Update.cshtml", result);
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