using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Nexus_Service_Marketing_System.Models;
using Nexus_Service_Marketing_System.Connection;
using Microsoft.EntityFrameworkCore;

namespace Nexus_Service_Marketing_System.Controllers
{
    public class VendorsController : Controller
    {
        private ModelsContext db = new ModelsContext();

        [Route("Admin/Vendors")]
        public IActionResult Index(string txtVendorName)
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin"))
            {
                TempData.Keep();

                List<vendors> results = db.vendors.ToList();

                if (results != null)
                {
                    if (txtVendorName != null)
                    {
                        results = results.Where(item => item.Vendor_name.ToLower().Contains(txtVendorName.Trim().ToLower())).ToList();

                        if (results.Count == 0)
                        {
                            ViewBag.Msg = "No results found!";
                        }
                        else
                        {
                            ViewBag.Msg = results.Count + " result(s) found!";
                        }
                    }

                    return View(@"~/Views/Admin/vendors/Index.cshtml", results);
                }

                return View(@"~/Views/Admin/ErrorNoDataFound.cshtml");
            }

            return View(@"~/Views/Admin/ErrorPemission.cshtml");
        }
        
        [Route("Admin/Vendors/Details/{id}")]        
        public IActionResult Details(int id)
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin"))
            {
                var result = db.vendors.Find(id);

                if (result != null)
                {
                    return View(@"~/Views/Admin/vendors/Details.cshtml", result);
                }

                return View(@"~/Views/Admin/vendors/Index.cshtml");
            }

            return View(@"~/Views/Admin/ErrorPemission.cshtml");
        }

        [HttpGet]
        [Route("Admin/Vendors/Create")]
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin"))
            {
                return View(@"~/Views/Admin/vendors/Create.cshtml");
            }

            return View(@"~/Views/Admin/ErrorPemission.cshtml");
        }

        [HttpPost]
        [Route("Admin/Vendors/Create")]
        public IActionResult Create(vendors vendor)
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin"))
            {
                try
                {
                    var result = db.vendors.FirstOrDefault(item => item.Vendor_id.Equals(vendor.Vendor_id));
                    var address = db.vendors.FirstOrDefault(item => item.Vendor_address.Equals(vendor.Vendor_address));

                    if (result == null)
                    {
                        if (address == null)
                        {
                            if (ModelState.IsValid)
                            {
                                db.vendors.Add(vendor);
                                db.SaveChanges();

                                return RedirectToAction("Index", "Vendors");
                            }
                            else
                            {
                                ViewBag.Msg = "Model State is invalid!";
                            }
                        }
                        else
                        {
                            ViewBag.Msg = "This vendor address has already existed!!";
                        }                        
                    }
                    else
                    {
                        ViewBag.Msg = "This vendor has already existed!";
                    }

                    return View(@"~/Views/Admin/vendors/Create.cshtml");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error: ", ex.Message);
                }
            }

            return View(@"~/Views/Admin/ErrorPemission.cshtml");
        }

        [HttpGet]
        [Route("Admin/Vendors/Update/{id}")]
        public IActionResult Update(int id)
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin"))
            {
                var result = db.vendors.Find(id);

                if (result != null)
                {
                    return View(@"~/Views/Admin/vendors/Update.cshtml", result);
                }

                return RedirectToAction("Index", "Vendors");
            }

            return View(@"~/Views/Admin/ErrorPemission.cshtml");
        }

        [HttpPost]        
        public IActionResult Update(vendors vendor)
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin"))
            {
                try
                {
                    var result = db.vendors.Find(vendor.Vendor_id);                    
                    var address = db.vendors.FirstOrDefault(item => item.Vendor_address.Equals(vendor.Vendor_address));

                    if (address != null && vendor.Vendor_address != result.Vendor_address)
                    {
                        ViewBag.Msg = "This vendor address has already existed!";

                        return View(@"~/Views/Admin/vendors/Update.cshtml", result);
                    }

                    if (ModelState.IsValid)
                    {
                        result.Vendor_name = vendor.Vendor_name;
                        result.Vendor_address = vendor.Vendor_address;

                        db.SaveChanges();

                        return RedirectToAction("Index", "Vendors");
                    }
                    else
                    {
                        ViewBag.thongbao = "Model State is invalid!";
                    }

                    return View(@"~/Views/Admin/vendors/Update.cshtml", result);
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