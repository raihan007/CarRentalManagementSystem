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
    public class HomeController : Controller
    {
        private DbContext _context;

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            if (Session["USER"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {
                _context = new DbContext();

                Login logger = _context.Logins.SingleOrDefault(d => d.Username == model.Email && d.Password == model.Password);
                
                if (logger != null)
                {
                    Employee emp = _context.Employees.SingleOrDefault(d => d.UserId == logger.UserId);
                    Customer cust = _context.Customers.SingleOrDefault(d => d.UserId == logger.UserId);
                    logger.Employee = emp;
                    logger.Customer = cust;
                    Session["USER"] = logger;
                    if (logger.Customer != null)
                    {
                        return RedirectToAction("ChooseCar","Reservation");
                    }
                    else if (logger.Employee.Role == "Manager")
                    {
                        return RedirectToAction("Index", "Manager");
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                    
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        public ActionResult LogOff()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterCustomerViewModel customer)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase photo = Request.Files["photoUpload"];
                var dir = Server.MapPath("~/App_Data/Images/Customers/");

                try
                {
                    _context = new DbContext();

                    if (photo != null)
                    {
                        var newPhoto = dir;
                        if (!Directory.Exists(newPhoto))
                        {
                            Directory.CreateDirectory(newPhoto);
                        }
                        newPhoto += "\\" + customer.Username + System.DateTime.Now.ToString("ddMMyyhhmmss") + photo.FileName;
                        customer.Photo = customer.Username + System.DateTime.Now.ToString("ddMMyyhhmmss") + photo.FileName;
                        photo.SaveAs(newPhoto);
                    }
                    else
                    {
                        customer.Photo = "";
                    }

                    var Customer = new Customer()
                    {
                        Name = customer.Name,
                        Email = customer.Email,
                        Phone = customer.Phone,
                        Gender = customer.Gender,
                        Address = customer.Address,
                        Photo = customer.Photo,
                        Birthdate = customer.Birthdate,
                        NationalId = customer.NationalId,
                        PassportNo = customer.PassportNo,
                        Login = new Login()
                        {
                            Username = customer.Username,
                            Password = customer.Password,
                            LastLoginTime = DateTime.Now
                        }
                    };
                    _context.Customers.Add(Customer);

                    _context.SaveChanges();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                TempData["regSuccessMessage"] = "Registration Successfully Done -. at " + DateTime.Now.ToShortTimeString() + "Please Login Here !";
                return RedirectToAction("Login","Home");
            }
            else
            {
                return View(customer);
            }
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        [System.Web.Services.WebMethod]
        public string CheckUsername(string username)
        {
            string retval = "";
            _context = new DbContext();
            Employee employee = _context.Employees.SingleOrDefault(d => d.Email == username);
            Customer customer = _context.Customers.SingleOrDefault(d => d.Email == username);
            Login logger = _context.Logins.SingleOrDefault(d => d.Username == username);
            if (logger != null || employee != null || customer != null)
            {
                retval = "true";
            }
            else
            {
                retval = "false";
            }

            return retval;
        } 
    }
}