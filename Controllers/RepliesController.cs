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
    public class RepliesController : Controller
    {
        private ModelsContext db = new ModelsContext();

        [Route("Admin/Replies")]
        public IActionResult Index()
        {
            TempData.Keep();

            List<replies> results = db.replies.ToList();

            return View(@"~/Views/Web/replies/Index.cshtml", results);
        }

        [HttpGet]
        [Route("Admin/Replies/Create")]
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("accId") != null)
            {
                return View(@"~/Views/Web/replies/Create.cshtml");
            }

            return View(@"~/Views/Admin/ErrorPemission.cshtml");
        }

        [HttpPost]
        [Route("Admin/Replies/Create")]
        public IActionResult Create(replies reply)
        {
            if (HttpContext.Session.GetString("accId") != null)
            {
                try
                {
                    var result = db.replies.FirstOrDefault(item => item.Reply_id.Equals(reply.Reply_id));

                    if (result == null)
                    {
                        if (ModelState.IsValid)
                        {
                            db.replies.Add(reply);
                            db.SaveChanges();
                        }
                        else
                        {
                            ViewBag.Msg = "Model State is invalid!";
                        }
                    }
                    else
                    {
                        ViewBag.Msg = "This reply has already existed!";
                    }

                    return View(@"~/Views/Web/replies/Create.cshtml");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error: ", ex.Message);
                }
            }

            return View(@"~/Views/Admin/ErrorPemission.cshtml");
        }

        [HttpGet]
        [Route("Admin/Replies/Update/{id}")]
        public IActionResult Update(int id)
        {
            if (HttpContext.Session.GetString("accId") != null)
            {
                var result = db.replies.Find(id);

                if (result != null)
                {
                    return View(@"~/Views/Web/replies/Update.cshtml", result);
                }

                return View(@"~/Views/Web/replies/Index.cshtml");
            }

            return View(@"~/Views/Admin/ErrorPemission.cshtml");
        }

        [HttpPost]
        public IActionResult Update(replies replie)
        {
            if (HttpContext.Session.GetString("accId") != null)
            {
                try
                {
                    var result = db.replies.Find(replie.Reply_id);

                    if (ModelState.IsValid)
                    {
                        result.Reply_content = replie.Reply_content;

                        db.SaveChanges();
                    }
                    else
                    {
                        ViewBag.Msg = "Model State is invalid!";
                    }

                    return View(@"~/Views/Web/replies/Update.cshtml", result);
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