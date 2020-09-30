using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Nexus_Service_Marketing_System.Connection;
using Nexus_Service_Marketing_System.Models;
using Nexus_Service_Marketing_System.Responsitory;
using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace Nexus_Service_Marketing_System.Controllers
{
    public class WebController : Controller
    {
        private ModelsContext db = new ModelsContext();

        [Route("/")]
        public IActionResult Index()
        {
            return View(@"~/Views/Web/Index.cshtml");
        }

        [HttpGet]
        [Route("Login")]
        public IActionResult Login()
        {
            var result = HttpContext.Session.GetString("accId");

            if (result != null)
            {
                return RedirectToAction("Index", "Web");
            }

            return View(@"~/Views/Web/Login.cshtml");
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(string txtAccId, string txtAccPwd)
        {
            try
            {
                var result = db.accounts.FirstOrDefault(item => item.Account_id.Equals(txtAccId));
                string PasswordMd5 = Encryptor.GetMd5Hash(txtAccPwd);

                if (result == null)
                {
                    ViewBag.Msg = "Invalid Account ID!";

                    return View(@"~/Views/Web/Login.cshtml");
                }

                if (!result.Account_password.Equals(PasswordMd5))
                {
                    ViewBag.Msg = "Invalid Account Password!";

                    return View(@"~/Views/Web/Login.cshtml");
                }

                if (result.Account_status.Equals("Inactive"))
                {
                    ViewBag.Msg = "Your account has been deactivated!";

                    return View(@"~/Views/Web/Login.cshtml");
                }

                HttpContext.Session.SetString("accId", result.Account_id);
                HttpContext.Session.SetString("accRole", result.Account_role);
                HttpContext.Session.SetString("storeId", result.Account_retailShowroom_id.ToString());

                return RedirectToAction("Index", "Web");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View(@"~/Views/Web/Login.cshtml");
        }

        [Route("Logout")]
        public IActionResult Logout()
        {
            var result = HttpContext.Session.GetString("accId");

            if (result != null)
            {
                HttpContext.Session.Remove("accId");
                HttpContext.Session.Remove("accName");
                HttpContext.Session.Remove("accRole");

                return RedirectToAction("Login", "Web");
            }

            return RedirectToAction("Login", "Web");
        }

        [Route("Services")]
        public IActionResult Services()
        {
            return View(@"~/Views/Web/Services.cshtml");
        }

        [Route("AboutUs")]
        public IActionResult AboutUs()
        {
            return View(@"~/Views/Web/AboutUs.cshtml");
        }

        [Route("ContactUs")]
        public IActionResult ContactUs()
        {
            return View(@"~/Views/Web/ContactUs.cshtml");
        }

        [Route("Equipments")]
        public IActionResult Equipments()
        {
            List<equipments> results = db.equipments.ToList();

            return View(@"~/Views/Web/Equipments.cshtml", results);
        }

        [Route("Equipments/{id}")]
        public IActionResult Equipments(int id)
        {
            equipments result = db.equipments.FirstOrDefault(item => item.Equipment_id.Equals(id));
            ViewBag.Feedbacks = db.feedbacks.Where(item => item.Feedback_equipment_id == id).ToList();            

            return View(@"~/Views/Web/EquipmentDetails.cshtml", result);
        }

        [Route("Plans")]
        public IActionResult Plans()
        {
            List<plans> results = db.plans.ToList();

            return View(@"~/Views/Web/Plans.cshtml", results);
        }

        [Route("Plans/{id}")]
        public IActionResult Plans(int id)
        {
            plans result = db.plans.FirstOrDefault(item => item.Plan_id.Equals(id));
            ViewBag.Feedbacks = db.feedbacks.Where(item => item.Feedback_plan_id == id).ToList();

            return View(@"~/Views/Web/PlanDetails.cshtml", result);
        }

        [Route("Cart")]
        public IActionResult Cart()
        {
            List<equipments_orders> cartEquipment = SessionHelper.GetObjectAsJson<List<equipments_orders>>(HttpContext.Session, "cartEquipment");
            List<plans_orders> cartPlan = SessionHelper.GetObjectAsJson<List<plans_orders>>(HttpContext.Session, "cartPlan");

            ViewBag.cartEquipment = cartEquipment;
            ViewBag.cartPlan = cartPlan;

            ViewBag.Count = 0;
            ViewBag.Total = 0;

            if (cartEquipment != null)
            {
                ViewBag.Count += cartEquipment.Sum(item => item.Equipments_orders_quantity);
                ViewBag.Total += cartEquipment.Sum(item => item.Equipments_orders_quantity * item.Equipments_orders_equipment_price);
            }

            if (cartPlan != null)
            {
                ViewBag.Count += cartPlan.Count;
                ViewBag.Total += cartPlan.Sum(item => item.Plans_orders_plan_price);
            }

            return View(@"~/Views/Web/Cart.cshtml");
        }

        [Route("Cart/AddEquipment")]
        public IActionResult AddEquipmentToCart(int txtEquipmentId, int txtQuantity)
        {
            List<equipments_orders> cartSession = SessionHelper.GetObjectAsJson<List<equipments_orders>>(HttpContext.Session, "cartEquipment");

            if (cartSession == null)
            {
                var cart = new List<equipments_orders>();

                cart.Add(new equipments_orders
                {
                    Equipments_orders_equipment_id = txtEquipmentId,
                    Equipments_orders_quantity = txtQuantity
                });

                SessionHelper.SetObjectAsJson(HttpContext.Session, "cartEquipment", cart);
            }
            else
            {
                for (var i = 0; i < cartSession.Count; i++)
                {
                    if (cartSession[i].Equipments_orders_equipment_id.Equals(txtEquipmentId))
                    {
                        cartSession[i].Equipments_orders_quantity += txtQuantity;

                        SessionHelper.SetObjectAsJson(HttpContext.Session, "cartEquipment", cartSession);

                        return RedirectToAction("Cart", "Web");
                    }
                }

                var item = new equipments_orders();
                item.Equipments_orders_equipment_id = txtEquipmentId;
                item.Equipments_orders_quantity = txtQuantity;

                cartSession.Add(item);

                SessionHelper.SetObjectAsJson(HttpContext.Session, "cartEquipment", cartSession);
            }

            return RedirectToAction("Cart", "Web");
        }

        [Route("Cart/AddPlan")]
        public IActionResult AddPlanToCart(int txtPlanId)
        {
            List<plans_orders> cartSession = SessionHelper.GetObjectAsJson<List<plans_orders>>(HttpContext.Session, "cartPlan");

            if (cartSession == null)
            {
                var cart = new List<plans_orders>();

                cart.Add(new plans_orders
                {
                    Plans_orders_plan_id = txtPlanId,
                });

                SessionHelper.SetObjectAsJson(HttpContext.Session, "cartPlan", cart);
            }
            else
            {
                var item = new plans_orders();
                item.Plans_orders_plan_id = txtPlanId;

                cartSession.Add(item);

                SessionHelper.SetObjectAsJson(HttpContext.Session, "cartPlan", cartSession);
            }

            return RedirectToAction("Cart", "Web");
        }

        [Route("Cart/RemoveEquipment/{id}")]
        public IActionResult RemoveEquipmentFromCart(int id)
        {
            List<equipments_orders> cartEquipment = SessionHelper.GetObjectAsJson<List<equipments_orders>>(HttpContext.Session, "cartEquipment");

            cartEquipment.RemoveAt(id);

            SessionHelper.SetObjectAsJson(HttpContext.Session, "cartEquipment", cartEquipment);

            return RedirectToAction("Cart", "Web");
        }

        [Route("Cart/RemovePlan/{id}")]
        public IActionResult RemovePlanFromCart(int id)
        {
            List<plans_orders> cartPlan = SessionHelper.GetObjectAsJson<List<plans_orders>>(HttpContext.Session, "cartPlan");

            cartPlan.RemoveAt(id);

            SessionHelper.SetObjectAsJson(HttpContext.Session, "cartPlan", cartPlan);

            return RedirectToAction("Cart", "Web");
        }

        [Route("Cart/QuantityPlus/{index}")]
        public IActionResult QuantityPlus(int index)
        {
            List<equipments_orders> cartEquipment = SessionHelper.GetObjectAsJson<List<equipments_orders>>(HttpContext.Session, "cartEquipment");

            cartEquipment[index].Equipments_orders_quantity += 1;

            SessionHelper.SetObjectAsJson(HttpContext.Session, "cartEquipment", cartEquipment);

            return RedirectToAction("Cart", "Web");
        }

        [Route("Cart/QuantityMinus/{index}")]
        public IActionResult QuantityMinus(int index)
        {
            List<equipments_orders> cartEquipment = SessionHelper.GetObjectAsJson<List<equipments_orders>>(HttpContext.Session, "cartEquipment");

            cartEquipment[index].Equipments_orders_quantity -= 1;

            SessionHelper.SetObjectAsJson(HttpContext.Session, "cartEquipment", cartEquipment);

            return RedirectToAction("Cart", "Web");
        }        

        [HttpPost]
        [Route("Feedbacks/Create")]
        public IActionResult FeedbackCreate(string txtAccId, int txtEquipmentId, int txtPlanId, string txtFeedbackTitle, string txtFeedbackContent)
        {
            if (HttpContext.Session.GetString("accRole").Equals("Customer"))
            {
                try
                {
                    feedbacks feedback = new feedbacks();

                    feedback.Feedback_account_id = txtAccId;                    
                    feedback.Feedback_title = txtFeedbackTitle;
                    feedback.Feedback_content = txtFeedbackContent;

                    if (txtEquipmentId != 0)
                    {
                        var equipment = db.equipments.Find(txtEquipmentId);                       

                        feedback.Feedback_equipment_id = txtEquipmentId;

                        db.feedbacks.Add(feedback);
                        db.SaveChanges();

                        ViewBag.Feedbacks = db.feedbacks.Where(item => item.Feedback_equipment_id == txtEquipmentId).ToList();

                        return View(@"~/Views/Web/EquipmentDetails.cshtml", equipment);
                    }

                    if (txtPlanId != 0)
                    {
                        var plan = db.plans.Find(txtPlanId);                        

                        feedback.Feedback_plan_id = txtPlanId;

                        db.feedbacks.Add(feedback);
                        db.SaveChanges();

                        ViewBag.Feedbacks = db.feedbacks.Where(item => item.Feedback_plan_id == txtPlanId).ToList();

                        return View(@"~/Views/Web/PlanDetails.cshtml", plan);
                    }                    
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error: ", ex.Message);
                }
            }

            return View(@"~/Views/Admin/ErrorPermission.cshtml");
        }

        [HttpPost]
        [Route("Replies/Create")]
        public IActionResult ReplyCreate(string txtAccId, int txtFeedbackId, int txtEquipmentId, int txtPlanId, string txtReplyContent)
        {
            if (HttpContext.Session.GetString("accId") != null)
            {
                try
                {
                    db.replies.Add(new replies()
                    {
                        Reply_account_id = txtAccId,
                        Reply_feedback_id = txtFeedbackId,
                        Reply_content = txtReplyContent
                    });

                    db.SaveChanges();

                    if (txtEquipmentId != 0)
                    {
                        var equipment = db.equipments.Find(txtEquipmentId);
                        ViewBag.Feedbacks = db.feedbacks.Where(item => item.Feedback_equipment_id == txtEquipmentId).ToList();

                        return View(@"~/Views/Web/EquipmentDetails.cshtml", equipment);
                    }
                    
                    if (txtPlanId != 0)
                    {
                        var plan = db.plans.Find(txtPlanId);
                        ViewBag.Feedbacks = db.feedbacks.Where(item => item.Feedback_plan_id == txtPlanId).ToList();

                        return View(@"~/Views/Web/PlanDetails.cshtml", plan);
                    }                    
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error: ", ex.Message);
                }
            }

            return View(@"~/Views/Admin/ErrorPermission.cshtml");
        }

        [Route("Orders")]
        public IActionResult Orders(string txtOrderId)
        {
            if (txtOrderId == null)
            {
                return View(@"~/Views/Web/Orders.cshtml");
            }

            var results = db.orders.Where(item => item.Order_id.ToLower().Contains(txtOrderId.Trim().ToLower())).ToList();            
            
            if (results != null)
            {
                ViewBag.Msg = results.Count + " result(s) found!";

                return View(@"~/Views/Web/Orders.cshtml", results);
            }
            else
            {
                ViewBag.Msg = "No results found!";

                return View(@"~/Views/Web/Orders.cshtml");
            }
        }

        [Route("Orders/Details/{id}")]
        public IActionResult OrderDetails(string id)
        {
            var equipments = db.equipments_orders.Where(item => item.Equipments_orders_order_id.Equals(id)).ToList();
            var plans = db.plans_orders.Where(item => item.Plans_orders_order_id.Equals(id)).ToList();

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

            return View(@"~/Views/Web/OrderDetails.cshtml");
        }

        [HttpGet]
        [Route("Orders/Create")]
        public IActionResult OrderCreate()
        {
            if (HttpContext.Session.GetString("accRole") != null)
            {
                if (HttpContext.Session.GetString("accRole").Equals("SalesPerson") || HttpContext.Session.GetString("accRole").Equals("Customer"))
                {
                    List<plans_orders> cartPlan = SessionHelper.GetObjectAsJson<List<plans_orders>>(HttpContext.Session, "cartPlan");

                    int? connectionId = 0;
                    var firstLetter = "NULL";
                    var number = 0;

                    if (cartPlan != null)
                    {
                        for (var i = 0; i < cartPlan.Count; i++)
                        {
                            if (cartPlan[i].Plans_orders_connection_id == 2)
                            {
                                firstLetter = "T";

                                break;
                            }
                        }

                        if (!firstLetter.Equals("T"))
                        {
                            connectionId = cartPlan[0].Plans_orders_connection_id;

                            if (connectionId == 1)
                            {
                                firstLetter = "D";
                            }
                            else
                            {
                                firstLetter = "B";
                            }
                        }
                    }
                    else
                    {
                        firstLetter = "O";
                    }

                    List<orders> results = db.orders.Where(item => item.Order_id.Substring(0, 1).Equals(firstLetter)).ToList();

                    if (results.Count == 0)
                    {
                        number = 1;
                    }
                    else
                    {
                        for (var i = 0; i < results.Count; i++)
                        {
                            if (number <= Convert.ToInt32(results[i].Order_id.Substring(1)))
                            {
                                number = Convert.ToInt32(results[i].Order_id.Substring(1)) + 1;
                            }
                        }
                    }

                    ViewBag.OrderId = String.Format(firstLetter + "{0:0000000000}", number);

                    return View(@"~/Views/Web/OrderCreate.cshtml");
                }

                return View(@"~/Views/Web/ErrorPermission.cshtml");
            }
            else
            {
                return RedirectToAction("Login", "Web");
            }
        }

        [HttpPost]
        [Route("Orders/Create")]
        public IActionResult OrderCreate(orders order)
        {
            if (HttpContext.Session.GetString("accRole").Equals("SalesPerson") || HttpContext.Session.GetString("accRole").Equals("Customer"))
            {
                List<equipments_orders> cartEquipment = SessionHelper.GetObjectAsJson<List<equipments_orders>>(HttpContext.Session, "cartEquipment");
                List<plans_orders> cartPlan = SessionHelper.GetObjectAsJson<List<plans_orders>>(HttpContext.Session, "cartPlan");                

                if (HttpContext.Session.GetString("accRole").Equals("Customer"))
                {
                    var account = db.accounts.Find(HttpContext.Session.GetString("accId"));                    

                    if (account != null)
                    {                        
                        order.Order_phone = account.Account_phone;
                        order.Order_address = account.Account_address;
                    }                    
                }

                if (HttpContext.Session.GetString("accRole").Equals("SalesPerson"))
                {
                    var checkPhone = db.accounts.FirstOrDefault(item => item.Account_phone.Equals(order.Order_phone));

                    if (checkPhone != null)
                    {
                        var checkRole = checkPhone.Account_role;

                        if (checkRole.Equals("Customer"))
                        {
                            order.Order_account_id = checkPhone.Account_id;
                            order.Order_phone = checkPhone.Account_phone;                            
                        }
                        else
                        {
                            ViewBag.Msg = "This phone number has been registered for an account in the system!";

                            return View(@"~/Views/Web/OrderCreate.cshtml");
                        }                        
                    }
                }

                db.orders.Add(order);
                db.SaveChanges();                

                if (cartEquipment != null)
                {
                    foreach (var item in cartEquipment)
                    {
                        var equipment = db.equipments.FirstOrDefault(temp => temp.Equipment_id == item.Equipments_orders_equipment_id);

                        equipment.Equipment_quantity -= cartEquipment.Sum(item => item.Equipments_orders_quantity);
                        item.Equipments_orders_order_id = order.Order_id;                        

                        db.equipments_orders.Add(item);
                        db.SaveChanges();
                    }

                    HttpContext.Session.Remove("cartEquipment");
                }
                
                if (cartPlan != null)
                {                   
                    foreach (var item in cartPlan)
                    {                        
                        var number = 0;
                        var results = db.plans_orders.Where(item => item.Plans_orders_id.Substring(0, 11).Equals(order.Order_id)).ToList();

                        if (results.Count == 0)
                        {
                            number = 1;
                        }
                        else
                        {
                            for (var i = 0; i < results.Count; i++)
                            {
                                if (number <= Convert.ToInt32(results[i].Plans_orders_id.Substring(12)))
                                {
                                    number = Convert.ToInt32(results[i].Plans_orders_id.Substring(12)) + 1;
                                }
                            }
                        }

                        item.Plans_orders_id = String.Format(order.Order_id + "-" + "{0:00}", number);
                        item.Plans_orders_order_id = order.Order_id;
                        item.Plans_orders_status = "Pending";
                        
                        db.plans_orders.Add(item);
                        db.SaveChanges();
                    }

                    HttpContext.Session.Remove("cartPlan");                    
                }           

                return RedirectToAction("Orders", "Web");
            }
            else
            {
                return RedirectToAction("Login", "Web");
            }
        }

        [Route("PlansOrders")]
        public IActionResult PlansOrders(string txtPlansOrdersId)
        {
            if (txtPlansOrdersId == null)
            {
                return View(@"~/Views/Web/PlansOrders.cshtml");
            }

            var results = db.plans_orders.Where(item => item.Plans_orders_id.ToLower().Contains(txtPlansOrdersId.Trim().ToLower())).ToList();

            if (results != null)
            {
                ViewBag.Msg = results.Count + " result(s) found!";

                return View(@"~/Views/Web/PlansOrders.cshtml", results);
            }
            else
            {
                ViewBag.Msg = "No results found!";

                return View(@"~/Views/Web/PlansOrders.cshtml");
            }
        }

        [Route("PlansOrders/Details/{id}")]
        public IActionResult PlansOrdersDetails(string id, DateTime txtPeriod)
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

                return View(@"~/Views/Web/PlansOrdersDetails.cshtml", results);
            }

            return View(@"~/Views/Web/ErrorNoDataFound.cshtml");
        }

        [Route("Bills")]
        public IActionResult Bills(string txtBillId)
        {
            if (txtBillId == null)
            {
                return View(@"~/Views/Web/Bills.cshtml");
            }

            var results = db.bills.Where(item => item.Bill_id.ToLower().Contains(txtBillId.Trim().ToLower())).ToList();

            if (results != null)
            {
                ViewBag.Msg = results.Count + " result(s) found!";

                return View(@"~/Views/Web/Bills.cshtml", results);
            }
            else
            {
                ViewBag.Msg = "No results found!";

                return View(@"~/Views/Web/Bills.cshtml");
            }
        }

        [Route("Bills/Details/{id}")]
        public IActionResult BillDetails(string id)
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

            return View(@"~/Views/Web/BillDetails.cshtml");
        }

        [Route("Profile/{id}")]
        public IActionResult Profile(string id)
        {
            if (HttpContext.Session.GetString("accId") != null)
            {
                var result = db.accounts.Find(id);

                if (result != null)
                {
                    return View(@"~/Views/Web/Profile.cshtml", result);
                }

                return RedirectToAction("Index", "Web");
            }

            return View(@"~/Views/Web/ErrorPermission.cshtml");
        }

        [HttpGet]
        [Route("Profile/Update/{id}")]
        public IActionResult ProfileUpdate(string id)
        {
            if (HttpContext.Session.GetString("accId") != null)
            {
                var result = db.accounts.Find(id);

                if (result != null)
                {
                    return View(@"~/Views/Web/ProfileUpdate.cshtml", result);
                }

                return RedirectToAction("Index", "Web");
            }

            return View(@"~/Views/Web/ErrorPermission.cshtml");
        }

        [HttpPost]
        [Route("Profile/Update/{id}")]
        public IActionResult ProfileUpdate(accounts account)
        {
            if (HttpContext.Session.GetString("accId") != null)
            {

                string PasswordMd5 = Encryptor.GetMd5Hash(account.Account_password);
                var ac = db.accounts.FirstOrDefault(m => m.Account_id.Equals(account.Account_id));
                var password = ac.Account_password;

                var phone = db.accounts.FirstOrDefault(item => item.Account_phone.Equals(account.Account_phone));
                var email = db.accounts.FirstOrDefault(item => item.Account_email.Equals(account.Account_email));

                try
                {
                    var result = db.accounts.Find(account.Account_id);

                    result.Account_name = account.Account_name;
                    if (!string.IsNullOrWhiteSpace(PasswordMd5))
                    {
                        if (password == null)
                        {
                            result.Account_password = PasswordMd5;
                        }
                    }
                    else
                    {
                        ViewBag.Msg = "Please enter Password";
                    }

                    if (phone == null)
                    {
                        result.Account_phone = account.Account_phone;
                    }
                    else
                    {
                        ViewBag.Msg = "This account phone has already existed!";
                    }
                    if (email == null)
                    {
                        result.Account_email = account.Account_email;
                    }
                    else
                    {
                        ViewBag.Msg = "This account email has already existed!";
                    }
                    
                    result.Account_address = account.Account_address;

                    db.SaveChanges();

                    return RedirectToAction("Index", "Web");                    
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error: ", ex.Message);
                }
            }

            return View(@"~/Views/Web/ErrorPermission.cshtml");
        }
    }
}