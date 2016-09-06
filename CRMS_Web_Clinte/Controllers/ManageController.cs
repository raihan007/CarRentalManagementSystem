using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using CRMS_Data;
using CRMS_Data.Entities;
using CRMS_Web_Clinte.Models;

namespace CRMS_Web_Clinte.Controllers
{
    public class ManageController : Controller
    {
        private DbContext _context;
        // GET: Manage
        public ActionResult Index()
        {
            if (Session["USER"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var model = new IndexViewModel
                {
                    HasPassword = true,
                    HasUsername = true,
                };
                return View(model);
            }
        }


        private bool HasPassword()
        {
            Login logger = (Login)Session["USER"];
            if (logger.Password != null)
            {
                return true;
            }
            return true;
        }

        private bool HasUsername()
        {
            Login logger = (Login)Session["USER"];
            if (logger.Username != null)
            {
                return true;
            }
            return true;
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            if (Session["USER"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                return View();
            }
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (Session["USER"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                Login logger = (Login)Session["USER"];
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                else
                {
                    _context = new DbContext();
                    var Login = _context.Logins.SingleOrDefault(m => m.UserId == logger.UserId);
                    if (model.OldPassword == Login.Password)
                    {
                        Login.Password = model.ConfirmPassword;

                        _context.SaveChanges();
                    }
                    else
                    {
                        TempData["Message"] = "Old Password Not Correct!";
                        return View(model);
                    }
                    TempData["Message"] = "Password Update Success. at " + DateTime.Now.ToShortTimeString();
                    return RedirectToAction("Index", "Manage");
                }
            }
        }
    }
}