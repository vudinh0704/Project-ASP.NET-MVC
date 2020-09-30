using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nexus_Service_Marketing_System.Connection;
using Nexus_Service_Marketing_System.Models;

namespace Nexus_Service_Marketing_System.Controllers
{
    public class EquipmentsController : Controller
    {
        private ModelsContext db = new ModelsContext();

        [Route("Admin/Equipments/")]
        public IActionResult Index(string txtEquipmentName)
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin") || HttpContext.Session.GetString("accRole").Equals("Technician"))
            {
                TempData.Keep();

                List<equipments> results = db.equipments.ToList();

                if (results != null)
                {
                    if (txtEquipmentName != null)
                    {
                        results = results.Where(item => item.Equipment_name.ToLower().Contains(txtEquipmentName.Trim().ToLower())).ToList();

                        if (results.Count == 0)
                        {
                            ViewBag.Msg = "No results found!";
                        }
                        else
                        {
                            ViewBag.Msg = results.Count + " result(s) found!";
                        }
                    }

                    return View(@"~/Views/Admin/equipments/Index.cshtml", results);
                }

                return View(@"~/Views/Admin/ErrorNoDataFound.cshtml");
            }

            return View(@"~/Views/Admin/ErrorPemission.cshtml");
        }

        [Route("Admin/Equipments/Details/{id}")]        
        public IActionResult Details(int id)
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin") || HttpContext.Session.GetString("accRole").Equals("Technician"))
            {
                var result = db.equipments.FirstOrDefault(item => item.Equipment_id.Equals(id));

                if (result != null)
                {
                    return View(@"~/Views/Admin/equipments/Details.cshtml", result);
                }

                return View(@"~/Views/Admin/equipments/Index.cshtml");
            }

            return View(@"~/Views/Admin/ErrorPemission.cshtml");
        }

        [HttpGet]
        [Route("Admin/Equipments/Create")]
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin") || HttpContext.Session.GetString("accRole").Equals("Technician"))
            {
                return View(@"~/Views/Admin/equipments/Create.cshtml");
            }

            return View(@"~/Views/Admin/ErrorPemission.cshtml");
        }

        [HttpPost]
        [Route("Admin/Equipments/Create")]
        public IActionResult Create(equipments equipment, IFormFile txtFileName)
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin") || HttpContext.Session.GetString("accRole").Equals("Technician"))
            {
                try
                {
                    var result = db.equipments.FirstOrDefault(item => item.Equipment_id.Equals(equipment.Equipment_id));
                    var vendor = db.vendors.FirstOrDefault(item => item.Vendor_id == equipment.Equipment_vendor_id);

                    if (vendor == null)
                    {
                        ViewBag.Msg = "This vendor id does not exist!";

                        return View(@"~/Views/Admin/equipments/Create.cshtml");
                    }

                    if (txtFileName == null)
                    {
                        ViewBag.Msg = "You must select the product's image!";

                        return View(@"~/Views/Admin/equipments/Create.cshtml");
                    }

                    if (result != null)
                    {
                        ViewBag.Msg = "This equipment has already existed!";

                        return View(@"~/Views/Admin/equipments/Create.cshtml");
                    }

                    string path = Path.Combine("wwwroot/assets/img/products/", txtFileName.FileName);
                    FileStream stream = new FileStream(path, FileMode.Create);

                    txtFileName.CopyToAsync(stream);
                    equipment.Equipment_image_thumbnail = txtFileName.FileName;

                    db.equipments.Add(equipment);
                    db.SaveChanges();

                    return RedirectToAction("Index", "Equipments");
                } 
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error: ", ex.Message);
                }               

                return View(@"~/Views/Admin/equipments/Create.cshtml");
            }

            return View(@"~/Views/Admin/ErrorPemission.cshtml");
        }

        [HttpGet]
        [Route("Admin/Equipments/Update/{id}")]
        public IActionResult Update(int id)
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin") || HttpContext.Session.GetString("accRole").Equals("Technician"))
            {
                var result = db.equipments.Find(id);

                if (result != null)
                {
                    return View(@"~/Views/Admin/equipments/Update.cshtml", result);
                }

                return RedirectToAction("Index", "Equipments");
            }

            return View(@"~/Views/Admin/ErrorPemission.cshtml");
        }

        [HttpPost]        
        public IActionResult Update(equipments equipment, IFormFile txtFileName)
        {
            if (HttpContext.Session.GetString("accRole").Equals("Admin") || HttpContext.Session.GetString("accRole").Equals("Technician"))
            {
                try
                {
                    var result = db.equipments.Find(equipment.Equipment_id);
                    var vendor = db.vendors.FirstOrDefault(item => item.Vendor_id == equipment.Equipment_vendor_id);

                    if (vendor == null)
                    {
                        ViewBag.Msg = "This vendor id does not exist!";

                        return View(@"~/Views/Admin/equipments/Update.cshtml", result);
                    }

                    if (txtFileName == null)
                    {
                        ViewBag.Msg = "You must select the product's image!";

                        return View(@"~/Views/Admin/equipments/Update.cshtml", result);
                    }

                    string path = Path.Combine("wwwroot/assets/img/products/", txtFileName.FileName);
                    FileStream stream = new FileStream(path, FileMode.Create);

                    txtFileName.CopyToAsync(stream);

                    result.Equipment_vendor_id = equipment.Equipment_vendor_id;
                    result.Equipment_name = equipment.Equipment_name;
                    result.Equipment_type = equipment.Equipment_type;
                    result.Equipment_content = equipment.Equipment_content;
                    result.Equipment_image_thumbnail = txtFileName.FileName;
                    result.Equipment_price = equipment.Equipment_price;
                    result.Equipment_quantity = equipment.Equipment_quantity;

                    db.SaveChanges();

                    return RedirectToAction("Index", "Equipments");
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