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
    public class FeedbacksController : Controller
    {
        private ModelsContext db = new ModelsContext();

        [Route("Admin/Feedbacks")]
        public IActionResult Index(string txtFeedbackTitle)
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin") || HttpContext.Session.GetString("accRole").Equals("SalesPerson") || HttpContext.Session.GetString("accRole").Equals("Technician") || HttpContext.Session.GetString("accRole").Equals("Accountant"))
            {
                TempData.Keep();

                List<feedbacks> results = db.feedbacks.ToList();

                if (results != null)
                {
                    if (txtFeedbackTitle != null)
                    {
                        results = results.Where(item => item.Feedback_title.ToLower().Contains(txtFeedbackTitle.Trim().ToLower())).ToList();

                        if (results.Count == 0)
                        {
                            ViewBag.Msg = "No results found!";
                        }
                        else
                        {
                            ViewBag.Msg = results.Count + " result(s) found!";
                        }
                    }

                    return View(@"~/Views/Admin/feedbacks/Index.cshtml", results);
                }

                return View(@"~/Views/Admin/ErrorNoDataFound.cshtml");
            }

            return View(@"~/Views/Admin/ErrorPemission.cshtml");
        }

        [Route("Admin/Feedbacks/{id}")]
        public IActionResult Details(int id)
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin") || HttpContext.Session.GetString("accRole").Equals("SalesPerson") || HttpContext.Session.GetString("accRole").Equals("Technician") || HttpContext.Session.GetString("accRole").Equals("Accountant"))
            {
                var result = db.feedbacks.FirstOrDefault(item => item.Feedback_id.Equals(id));

                if (result != null)
                {
                    return View(@"~/Views/Admin/feedbacks/Details.cshtml", result);
                }

                return View(@"~/Views/Admin/feedbacks/Index.cshtml");
            }

            return View(@"~/Views/Admin/ErrorPemission.cshtml");
        }

        [HttpGet]
        public IActionResult Create(int id)
        {
            ViewBag.FeedbackId = id;

            return View(@"~/Views/Admin/feedbacks/Create.cshtml");
        }

        [HttpPost]
        public IActionResult Create(replies replie)
        {

            var model = db.replies.FirstOrDefault(m => m.Reply_id.Equals(replie.Reply_id));

            if (model == null)
            {
                //if (ModelState.IsValid)
                //{


                db.replies.Add(replie);
                db.SaveChanges();
                ViewBag.msg = "The reply has been added";

                //}
                //else
                //{
                //    ViewBag.msg = "Failed to add item";

                //    }
            }
            else
            {
                ViewBag.msg = "Failed";
            }

            return View(@"~/Views/Admin/feedbacks/Create.cshtml", model);
        }

        [HttpGet]
        [Route("Admin/Feedbacks/Update/{id}")]
        public IActionResult Update(int id)
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin") || HttpContext.Session.GetString("accRole").Equals("SalesPerson"))
            {
                var result = db.feedbacks.Find(id);

                if (result != null)
                {
                    return View(@"~/Views/Admin/feedbacks/Update.cshtml", result);
                }

                return RedirectToAction("Index", "Feedbacks");
            }

            return View(@"~/Views/Admin/ErrorPemission.cshtml");
        }

        [HttpPost]
        public IActionResult Update(feedbacks feedback)
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin") || HttpContext.Session.GetString("accRole").Equals("SalesPerson"))
            {
                try
                {
                    var result = db.feedbacks.Find(feedback.Feedback_id);

                    if (ModelState.IsValid)
                    {
                        result.Feedback_title = feedback.Feedback_title;
                        result.Feedback_content = feedback.Feedback_content;
                        result.Feedback_account_id = feedback.Feedback_account_id;

                        db.SaveChanges();

                        return RedirectToAction("Index", "Feedbacks");
                    }
                    else
                    {
                        ViewBag.Msg = "Model State is invalid!";
                    }

                    return View(@"~/Views/Admin/feedbacks/Update.cshtml", result);
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