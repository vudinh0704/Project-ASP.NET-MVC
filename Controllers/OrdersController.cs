using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nexus_Service_Marketing_System.Models;
using Nexus_Service_Marketing_System.Connection;
using Microsoft.AspNetCore.Razor.Language;

namespace Nexus_Service_Marketing_System.Controllers
{
    public class OrdersController : Controller
    {
        private ModelsContext db = new ModelsContext();

        [Route("Admin/Orders")]
        public IActionResult Index(string txtOrderId)
        {
            TempData.Keep();

            if (HttpContext.Session.GetString("accRole").Equals("Admin") || HttpContext.Session.GetString("accRole").Equals("Accountant") || HttpContext.Session.GetString("accRole").Equals("Technician") || HttpContext.Session.GetString("accRole").Equals("SalesPerson"))
            {
                var results = db.orders.ToList();

                if (results != null)
                {
                    if (txtOrderId != null)
                    {
                        results = results.Where(item => item.Order_id.ToLower().Contains(txtOrderId.Trim().ToLower())).ToList();

                        if (results.Count == 0)
                        {
                            ViewBag.Msg = "No results found!";
                        }
                        else
                        {
                            ViewBag.Msg = results.Count + " result(s) found!";
                        }
                    }

                    return View(@"~/Views/Admin/Orders/Index.cshtml", results);
                }

                return View(@"~/Views/Admin/ErrorNoDataFound.cshtml");
            }

            return View(@"~/Views/Admin/ErrorPemission.cshtml");
        }        

        [Route("Admin/Orders/Details/{id}")]
        public IActionResult Details(string id)
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin") || HttpContext.Session.GetString("accRole").Equals("Accountant") || HttpContext.Session.GetString("accRole").Equals("Technician") || HttpContext.Session.GetString("accRole").Equals("SalesPerson"))
            {
                var equipments = db.equipments_orders.Where(item => item.Equipments_orders_order_id.Equals(id)).ToList();
                var plans = db.plans_orders.Where(item => item.Plans_orders_order_id.Equals(id)).ToList();

                if (equipments == null && plans == null)
                {
                    return RedirectToAction("Index", "Orders");
                }

                ViewBag.Count = 0;
                ViewBag.Total = 0;
                ViewBag.Equipments = equipments;
                ViewBag.Plans = plans;

                if (equipments != null)
                {
                    ViewBag.Count += equipments.Sum(item => item.Equipments_orders_quantity);
                    ViewBag.Total += equipments.Sum(item => item.Equipments_orders_quantity * item.Equipments_orders_equipment_price);
                }

                if (plans != null)
                {
                    ViewBag.Count += plans.Count();
                    ViewBag.Total += plans.Sum(item => item.Plans_orders_plan_price);
                }

                return View(@"~/Views/Admin/orders/Details.cshtml");
            }

            return View(@"~/Views/Admin/ErrorPemission.cshtml");
        }

        [HttpGet]
        [Route("Admin/Orders/Update/{id}")]
        public IActionResult Update(string id)
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin") || HttpContext.Session.GetString("accRole").Equals("Technician"))
            {
                var result = db.orders.Find(id);

                if (result != null)
                {
                    if (!result.Order_status.Equals("Complete"))
                    {
                        return View(@"~/Views/Admin/Orders/Update.cshtml", result);
                    }

                    ViewBag.Alert = "Order with ID " + id + " can no longer be edited because it has been completed!";
                }

                return RedirectToAction("Index", "Orders");
            }

            return View(@"~/Views/Admin/ErrorPemission.cshtml");
        }

        [HttpPost]        
        public IActionResult Update(orders order)
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin") || HttpContext.Session.GetString("accRole").Equals("Technician"))
            {
                try
                {
                    var result = db.orders.FirstOrDefault(item => item.Order_id.Equals(order.Order_id));
                    
                    if (db.bills.FirstOrDefault(item => item.Bill_order_id.Equals(order.Order_id)) == null)
                    {
                        result.Order_feasibility = order.Order_feasibility;
                        result.Order_status = order.Order_status;

                        if (result.Order_status.Equals("Complete"))
                        {
                            var plans = db.plans_orders.Where(item => item.Plans_orders_order_id.Equals(order.Order_id)).ToList();

                            foreach (var item in plans)
                            {
                                item.Plans_orders_status = "Connected";
                            }
                        }

                        db.SaveChanges();

                        return RedirectToAction("Index", "Orders");
                    }
                    else
                    {
                        ViewBag.Msg = "This order can not be edited anymore because it has been billed!";
                    }

                    return View(@"~/Views/Admin/orders/Update.cshtml", result);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error: ", ex.Message);
                }                
            }

            return View(@"~/Views/Admin/ErrorPemission.cshtml");
        }

        [HttpPost]
        [Route("Admin/Orders")]
        public IActionResult CreateAccountCustomer(string txtOrderId)
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin") || HttpContext.Session.GetString("accRole").Equals("Technician"))
            {
                if (txtOrderId != null)
                {
                    var retailShowroomId = db.orders.Find(txtOrderId).Order_retailShowroom_id;
                    var cityId = db.retailShowrooms.Find(retailShowroomId).RetailShowroom_city_id;
                    var order = db.orders.Find(txtOrderId);
                    var check = order.Order_account_id;
                    var accId = txtOrderId.Substring(0, 1) + cityId + txtOrderId;
                    var accPwd = "5f4dcc3b5aa765d61d8327deb882cf99";
                    var accPhone = db.orders.Find(txtOrderId).Order_phone;
                    var accAddress = db.orders.Find(txtOrderId).Order_address;                    
                    
                    if (check != null)
                    {
                        TempData["Notification"] = "This order has added the orderer information!!";
                        TempData.Keep();

                        return RedirectToAction("Index", "Orders");
                    }
                    else
                    {
                        order.Order_account_id = accId;

                        db.accounts.Add(new accounts()
                        {
                            Account_id = accId,                            
                            Account_password = accPwd,
                            Account_phone = accPhone,
                            Account_email = "default@gmail.com",
                            Account_address = accAddress,
                            Account_status = "Active",
                            Account_role = "Customer"                            
                        });

                        db.SaveChanges();

                        TempData["Notification"] = "Successfully create the customer account of order with ID: " + txtOrderId + ". Please provide this account information (ID: " + accId + ", Password: password) to the customer.";
                        TempData.Keep();                        
                        
                        return RedirectToAction("Index", "Orders");
                    }
                }
            }

            return View(@"~/Views/Admin/ErrorPemission.cshtml");
        }
    }
}
