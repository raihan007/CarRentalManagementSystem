using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Web.Security;
using CRMS_Data;
using CRMS_Data.Entities;
using CRMS_Web_Clinte.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace CRMS_Web_Clinte.Controllers
{
    public class VehicleController : Controller
    {
        private DbContext _context;
        private byte[] _bytes;
        private String _image;
        private HttpPostedFileBase photo;
        private List<Vehicle> allVehicles;

        private string avater ="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";

        [HttpGet]
        public ActionResult Index()
        {
            String SearchKey = Request["SearchString"];
            _context = new DbContext();
            var dir = Server.MapPath("~/App_Data/Images/Vehicles/");

           
            allVehicles = _context.Vehicles.ToList();

            if (!String.IsNullOrEmpty(SearchKey))
            {
                allVehicles = _context.Vehicles.Where(s => s.VehicleName.Contains(SearchKey)
                                       || s.Brand.Contains(SearchKey)).ToList();
            }

            List<Vehicle> vehicleList = new List<Vehicle>();
            foreach (var vehicle in allVehicles)
            {
                if (string.IsNullOrEmpty(vehicle.Photo))
                {
                    _image = avater;
                }
                else
                {
                    if (System.IO.File.Exists(dir + vehicle.Photo))
                    {
                        _bytes = System.IO.File.ReadAllBytes(dir + vehicle.Photo);
                        _image = Convert.ToBase64String(_bytes);
                    }
                    else
                    {
                        _image = avater;
                    }
                }
                Vehicle v = new Vehicle()
                {
                    VehicleId = vehicle.VehicleId,
                    VehicleName = vehicle.VehicleName,
                    Brand = vehicle.Brand,
                    Photo = _image,
                    Details = vehicle.Details,
                    Capacity = vehicle.Capacity,
                    CostPerHour = vehicle.CostPerHour,

                };
                vehicleList.Add(v);

            }

            return View(vehicleList);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context = new DbContext();
                    var dir = Server.MapPath("~/App_Data/Images/Vehicles/");
                    if (Request.Files["photoUpload"] != null)
                    {
                        photo = Request.Files["photoUpload"];
                    }


                    if (photo == null)
                    {
                        vehicle.Photo = "";

                    }
                    else
                    {
                        var newPhoto = dir;
                        if (!Directory.Exists(newPhoto))
                        {
                            Directory.CreateDirectory(newPhoto);
                        }

                        newPhoto += "\\" + System.DateTime.Now.ToString("ddMMyyhhmmss") + photo.FileName;
                        vehicle.Photo = System.DateTime.Now.ToString("ddMMyyhhmmss") + photo.FileName;
                        photo.SaveAs(newPhoto);
                    }

                    _context.Vehicles.Add(vehicle);
                    _context.SaveChanges();
                    TempData["Message"] = "Vehicle Added Successfully. at " + DateTime.Now.ToShortTimeString();
                    return RedirectToAction("Index");
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}",
                                                    validationError.PropertyName,
                                                    validationError.ErrorMessage);
                        }
                    }
                    return View(vehicle);
                }
            }
            else
            {
                return View(vehicle);
            }
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            _context = new DbContext();
            var dir = Server.MapPath("~/App_Data/Images/Vehicles/");

            Vehicle vehicle = _context.Vehicles.SingleOrDefault(d => d.VehicleId == id);
            if (vehicle.Photo == null || vehicle.Photo == "")
            {
                vehicle.Photo = "";
            }
            else
            {
                if (System.IO.File.Exists(dir + vehicle.Photo))
                {
                    _bytes = System.IO.File.ReadAllBytes(dir + vehicle.Photo);
                    _image = Convert.ToBase64String(_bytes);
                }
                else
                {
                    _image = "";
                }
                vehicle.Photo = _image;
            }

            return View(vehicle);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            _context = new DbContext();
            var dir = Server.MapPath("~/App_Data/Images/Vehicles/");

            Vehicle vehicle = _context.Vehicles.SingleOrDefault(d => d.VehicleId == id);
            if (vehicle.Photo == null || vehicle.Photo == "")
            {
                vehicle.Photo = "";
            }
            else
            {
                if (System.IO.File.Exists(dir + vehicle.Photo))
                {
                    _bytes = System.IO.File.ReadAllBytes(dir + vehicle.Photo);
                    _image = Convert.ToBase64String(_bytes);
                }
                else
                {
                    _image = "";
                }
                vehicle.Photo = _image;
            }

            return View(vehicle);
        }
        [HttpPost]
        public ActionResult Edit(Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context = new DbContext();
                    Vehicle oldVehicle = _context.Vehicles.SingleOrDefault(d => d.VehicleId == vehicle.VehicleId);
                    var dir = Server.MapPath("~/App_Data/Images/Vehicles/");
                    var httpPostedFileBase = Request.Files["photoUpload"];
                    if (httpPostedFileBase != null && (httpPostedFileBase.FileName != null || httpPostedFileBase.FileName != ""))
                    {
                        photo = Request.Files["photoUpload"];
                    }

                    if (photo.FileName == null && photo.FileName == "")
                    {
                        vehicle.Photo = "";

                    }else
                    {
                        if (vehicle.Photo == null && photo.FileName == "")
                        {
                            vehicle.Photo = oldVehicle.Photo;
                        }
                        else
                        {
                            var newPhoto = dir;
                            if (!Directory.Exists(newPhoto))
                            {
                                Directory.CreateDirectory(newPhoto);
                            }

                            newPhoto += "\\" + System.DateTime.Now.ToString("ddMMyyhhmmss") + photo.FileName;
                            vehicle.Photo = System.DateTime.Now.ToString("ddMMyyhhmmss") + photo.FileName;
                            photo.SaveAs(newPhoto);
                        }
                    }
                    if (oldVehicle != null)
                    {
                        oldVehicle.VehicleName = vehicle.VehicleName;
                        oldVehicle.Brand = vehicle.Brand;
                        oldVehicle.Photo = vehicle.Photo;
                        oldVehicle.Details = vehicle.Details;
                        oldVehicle.Capacity = vehicle.Capacity;
                        oldVehicle.CostPerHour = vehicle.CostPerHour;

                        _context.SaveChanges();
                        TempData["Message"] = "Vehicle Info Updated Successfully. at " + DateTime.Now.ToShortTimeString();
                    }
                    
                    return RedirectToAction("Index");
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}",
                                                    validationError.PropertyName,
                                                    validationError.ErrorMessage);
                        }
                    }
                    return View(vehicle);
                }
            }
            else
            {
                return View(vehicle);
            }
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            _context = new DbContext();
            var dir = Server.MapPath("~/App_Data/Images/Vehicles/");

            Vehicle vehicle = _context.Vehicles.SingleOrDefault(d => d.VehicleId == id);
            if (vehicle.Photo == null || vehicle.Photo == "")
            {
                vehicle.Photo = "";
            }
            else
            {
                if (System.IO.File.Exists(dir + vehicle.Photo))
                {
                    _bytes = System.IO.File.ReadAllBytes(dir + vehicle.Photo);
                    _image = Convert.ToBase64String(_bytes);
                }
                else
                {
                    _image = "";
                }
                vehicle.Photo = _image;
            }

            return View(vehicle);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult Delete_Post(int id)
        {
            _context = new DbContext();
            Vehicle vehicletodel = _context.Vehicles.SingleOrDefault(d => d.VehicleId == id);
            var dir = Server.MapPath("~/App_Data/Images/Vehicles/");
            if (System.IO.File.Exists(dir + vehicletodel.Photo))
            {
                System.IO.File.Delete(dir + vehicletodel.Photo);
            }
            _context.Vehicles.Remove(vehicletodel);
            _context.SaveChanges();
            TempData["Message"] = "Successfully Deleted. at " + DateTime.Now.ToShortTimeString();
            return RedirectToAction("Index");
        }
    }
}