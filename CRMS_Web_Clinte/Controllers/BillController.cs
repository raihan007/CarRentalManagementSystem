using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRMS_Data;
using CRMS_Data.Entities;

namespace CRMS_Web_Clinte.Controllers
{
    public class BillController : Controller
    {
        private DbContext _context;
        private byte[] _bytes;
        private String _image;
        private HttpPostedFileBase photo;

        [HttpGet]
        public ActionResult Index()
        {
            _context = new DbContext();

            var allBills = _context.Bills.ToList();

            return View(allBills);
        }
    }
}