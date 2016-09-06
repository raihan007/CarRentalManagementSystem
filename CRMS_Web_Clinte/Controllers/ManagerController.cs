using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRMS_Data;
using CRMS_Data.Entities;
using CRMS_Web_Clinte.Models;

namespace CRMS_Web_Clinte.Controllers
{
    public class ManagerController : Controller
    {
        private DbContext _context;
        // GET: Admin
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["USER"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                _context = new DbContext();
                var dashborad = new DashboardViewModel()
                {
                    TotalCustomers = _context.Customers.Count(),
                    TotalVaicles = _context.Vehicles.Count(),
                    TotalEmployee = _context.Employees.Count(),
                    TotalOngingRes = _context.Reservations.Count(),
                    TotalPendingRes = _context.Reservations.Count(),
                };
                return View(dashborad);
            }
        } 
    }
}