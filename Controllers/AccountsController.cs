using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nexus_Service_Marketing_System.Connection;
using Nexus_Service_Marketing_System.Models;
using Nexus_Service_Marketing_System.Responsitory;

namespace Nexus_Service_Marketing_System.Controllers
{
    public class AccountsController : Controller
    {
        private ModelsContext db = new ModelsContext();

        [Route("Admin/Accounts")]
        public IActionResult Index(string txtAccId)
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin"))
            {
                List<accounts> results = db.accounts.OrderBy(item => item.Account_role).ToList();

                if (results != null)
                {
                    if (txtAccId != null)
                    {
                        results = results.Where(item => item.Account_id.ToLower().Contains(txtAccId.Trim().ToLower())).OrderBy(item => item.Account_role).ToList();

                        if (results.Count == 0)
                        {
                            ViewBag.Msg = "No results found!";
                        }
                        else
                        {
                            ViewBag.Msg = results.Count + " result(s) found!";
                        }                        
                    }
                    
                    return View(@"~/Views/Admin/accounts/Index.cshtml", results);
                }                

                return View(@"~/Views/Admin/ErrorNoDataFound.cshtml");
            }

            return View(@"~/Views/Admin/ErrorPemission.cshtml");
        }

        [Route("Admin/Accounts/Details/{id}")]
        public IActionResult Details(string id)
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin"))
            {
                var result = db.accounts.Find(id);

                if (result != null)
                {
                    return View(@"~/Views/Admin/accounts/Details.cshtml", result);
                }

                return View(@"~/Views/Admin/accounts/Index.cshtml");
            }

            return View(@"~/Views/Admin/ErrorPemission.cshtml");
        }

        [HttpGet]
        [Route("Admin/Accounts/Create")]
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin"))
            {                
                return View(@"~/Views/Admin/accounts/Create.cshtml");
            }

            return View(@"~/Views/Admin/ErrorPemission.cshtml");
        }

        [HttpPost]
        [Route("Admin/Accounts/Create")]
        public IActionResult Create(accounts account)
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin"))
            {
                try
                {
                    var id = db.accounts.FirstOrDefault(item => item.Account_id.Equals(account.Account_id));                    
                    var phone = db.accounts.FirstOrDefault(item => item.Account_phone.Equals(account.Account_phone));
                    var email = db.accounts.FirstOrDefault(item => item.Account_email.Equals(account.Account_email));

                    if (id == null && phone == null && email == null)
                    {
                        if (ModelState.IsValid)
                        {
                            var PasswordMd5 = Encryptor.GetMd5Hash(account.Account_password);
                            account.Account_password = PasswordMd5;

                            db.accounts.Add(account);
                            db.SaveChanges();

                            return RedirectToAction("Index", "Accounts");
                        }
                        else
                        {
                            ViewBag.Msg = "Model State is invalid!";
                        }
                    }

                    if (id != null)
                    {
                        ViewBag.Msg = "This account id has already existed!";
                    }

                    if (phone != null)
                    {
                        ViewBag.Msg = "This account phone has already existed!";
                    }

                    if (email != null && !email.Equals("default@gmail.com"))
                    {
                        ViewBag.Msg = "This account email has already existed!";
                    }

                    if (HttpContext.Session.GetString("accRole").Equals("Technician"))
                    {
                        return View(@"~/Views/Admin/Orders.cshtml");
                    }

                    return View(@"~/Views/Admin/accounts/Create.cshtml");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error: ", ex.Message);
                }                
            }

            return View(@"~/Views/Admin/ErrorPemission.cshtml");
        }

        [HttpGet]
        [Route("Admin/Accounts/Update/{id}")]
        public IActionResult Update(string id)
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin"))
            {
                var result = db.accounts.Find(id);

                if (result != null)
                {
                    return View(@"~/Views/Admin/Accounts/Update.cshtml", result);
                }

                return RedirectToAction("Index", "Accounts");
            }

            return View(@"~/Views/Admin/ErrorPemission.cshtml");
        }

        [HttpPost]
        public IActionResult Update(accounts account)
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin"))
            {
                try
                {
                    var result = db.accounts.Find(account.Account_id);
                    var phone = db.accounts.FirstOrDefault(item => item.Account_phone.Equals(account.Account_phone));
                    var email = db.accounts.FirstOrDefault(item => item.Account_email.Equals(account.Account_email));

                    if (phone != null && account.Account_phone != result.Account_phone)
                    {
                        ViewBag.Msg = "This account phone has already existed!";

                        return View(@"~/Views/Admin/accounts/Update.cshtml", result);
                    }

                    if (email != null && account.Account_email != result.Account_email)
                    {
                        ViewBag.Msg = "This account phone has already existed!";

                        return View(@"~/Views/Admin/accounts/Update.cshtml", result);
                    }

                    var PasswordMd5 = Encryptor.GetMd5Hash(account.Account_password);

                    if (ModelState.IsValid)
                    {
                        result.Account_name = account.Account_name;
                        result.Account_password = PasswordMd5;
                        result.Account_role = account.Account_role;
                        result.Account_phone = account.Account_phone;
                        result.Account_address = account.Account_address;
                        result.Account_status = account.Account_status;

                        db.SaveChanges();

                        return RedirectToAction("Index", "Accounts");
                    }
                    else
                    {
                        ViewBag.Msg = "Model State is invalid!";
                    }

                    return View(@"~/Views/Admin/accounts/Update.cshtml", result);
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