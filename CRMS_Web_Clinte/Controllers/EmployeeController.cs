using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRMS_Data;
using CRMS_Data.Entities;

namespace CRMS_Web_Clinte.Controllers
{
    public class EmployeeController : Controller
    {
        private DbContext _context;
        private HttpPostedFileBase photo;
        private byte[] bytes;
        private string image;
        private List<Employee> allEmployees; 
        private string avater = "R0lGODlhAAEAAcYAAAQCBISChERCRMTCxCQiJKSipGRiZOTi5BQSFJSSlFRSVNTS1DQyNLSytHRydPTy9AwKDIyKjExKTMzKzC" +
                                 "wqLKyqrGxqbOzq7BwaHJyanFxaXNza3Dw6PLy6vHx6fPz6/AQGBISGhERGRMTGxCQmJKSmpGRmZOTm5BQWFJSWlFRWVNTW1DQ2NLS2tHR" +
                                 "2dPT29AwODIyOjExOTMzOzCwuLKyurGxubOzu7BweHJyenFxeXNze3Dw+PLy+vHx+fPz+/P///wAAAAAAAAAAAAAAAAAAAAAAAAAAAA" +
                                 "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" +
                                 "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACH5BAEAAEAALAAAA" +
                                 "AAAAQABAAf+gECCg4SFhoeIiYqLjI2Oj5CRkpOUlZaXmJmam5ydnp+goaKjpKWmp6ipqqusra6vsLGys7S1tre4ubq7vL2+v8DBwsPExcbHyMnKy8zNzs/Q0dKpHzc" +
                                 "bFT4aEiwUKAQsEjoeKS07Dy/T6cEfJx02PCgA8vP09QAoHBYlGw/q/rYfFsRQEM+ewYPyQPCIseLDv4etLpSQAAOhxYsIZFToB7EjqQUuSFwcORKEiAEOParkdMBGQZ" +
                                 "IwLcIwcWClTUsPQmCIyXMkiRk3gz760YJAz6MYWwhdmuiFBxBIoyIEkSMl06s3NEjdehBChatXHyjgStYeih5ghX6wULYtPRL+C9LerAHBrV0AHG7IVfmAw927Nqzu/dc" +
                                 "D6l+7JQZDDHH4LooVitP9uLBDa2O7PNBFfvahh4pul/8G2Ozsg4+6oQ/DGEGaWQLDqQ8z0Nsa2Y6dsS87qI3MRe7QOGryJnbC6O/LOYYTq3E8tAjBynv9sNz8MIod0YH" +
                                 "dMF79cIPsvxZ01w3eV4rxjQVoLp9rOvrDBC6w1/VB5Pu7ILDPx3Xi/uEO++HSgX9/ZRDgLTEQeJcPB9pigIJ2GdBgLX5B2JYM0E3oCg4WtpWZhrG8AFuHWzGwHoit7EB" +
                                 "iWSRwhGIrM6xIFgq0vcjKgDJuBUONNqrCXI5bydfjKiUAuZVwQ6b+koORUm2QpCoZMBnVBE8qKSVSA1SJSpRX9qSUlqYs2SVPX4FZiphjwpScmaRUkGZMMbBJyo9vjjS" +
                                 "anKLgWOdFu+EZip57InSnn5+IF6hFBRAKioiHIkSlop8w0KhBMAgJaScmTGoPDSdeqgmamsqjwg+edmJoqPJ4UGonLyCAqjwJrMrJBzy8CgAFMVgqqyUfiGCrPAzEtWslJ9i" +
                                 "A2q80nDDsJCfU+is9LiwbyQdjPUsPAcpK60gNI1oLAlraNlKttfTEGu4iF3BHrjyDnovICi+tC4Cq7iayQEXyyhNBvYlsEO+6ifJ7yAX2yQsCawIf4mu+wSV8SAT5AiCCw4f" +
                                 "+zIDvuuZSTEiv8iKgn8aENNCtrSqAbMgLzj4LwqMmEzLByKFq0PIhDjwLA5IzD4Lyr4nlbMgONKAKgg8Z+gzEAkFPCgHRRiOygwSHIiBCC0U3LcgDKUj6JggtdGr1ITfMwNaYOFT9tSGnXlny" +
                                 "2Y08kPSVa7LNyNhXOik3Iy10CQOpdy9ywL85CtA3Iy9ALWW0gy8CsZRlJp7Iy1JC5ngiN7ydIwouTn5Ipkaqp3kidOZoweeJ3GZk3KQX8oHhObKceiEJAIlCtq8XssLFJH5YeyE/VLhin7vDnmMNwf+MO4Qw2F08IaxbyEHmyw" +
                                 "PhJokMRk/IC+oSeLD1hSwOIQ3+0Ft/AuDv7ct9IQkqiAPt5wtyAwsKttu+IB24el9e8/NewPHN4aB8/oP4gfe60zgAXs93zeGUAQ+RvurYYIGHaAHMUtMzCBJiBsc6DsIsOAjIVedjHASCyKoDAvZx8DzVgYAJLUi346gwhII4QMGOA" +
                                 "wKcAfADBygAByYYmxRc4AV8m19laLCDFGSvOSiIwQQ2EMTovaAAHEqBIE7gAvKFBgQyyNINZBCAFdbuADqASosIsQAVZPAyJumAVToAAxpUwGyO+0ENKDAPAxXiBR1Y2GV40IBOvUArIFCAsFL3ABdcDAdeE8QLSmA5t7BgI4gYAb4QEI" +
                                 "LwDW4DKYPVIm7+EAMrIoUEBeCRIdwzDxZkKY4NsN88cCBKRFzAAWc8CgFSkEhD9CCDILBBK79WyJHJbxErMCNScBADSyKCVvagQA/g2LKnGeQ6kfhBB3jAQ3vgIAS6ckQBDAICD2TTZyNo5DxM0MRHPFGc9sCAB0AIiRvgxh4s2KDPKvDOemyvEhe" +
                                 "IQD3pgQIHSK4SNkAIClLATH4lgH/zAAFQLrEDG+AOAhZYQTkn4QOLgMAAXhTYDwJQTQBIERPSxBcJJJqJF6CzHjRYKMU+ULORPA+kHkio6yxRgo7KAwFfStgLfAOT71xiBqoEAPgy0byLUKWgq/pACGw6jxBc4gMqsIcLkLr+CD2SBAQBqOWubhCBWFq" +
                                 "kgJNoAKXAZYkBXjUAVPXTA3KgNZ78cxImPQgDMvoID8YEAimYqKdeUAEWMLUeOPgmJBpokKla4gUc6gkMKqBXQl0gATT4" +
                                 "qz08R4kdeBIAEMgpJXSAFBBYYJds+sECTHBZkpjAEg+6CA0E+4htRoUAmjXTC1YQgrZy5aOTuCVJAlOJGG1FAzNI635uM" +
                                 "IEQCECyJJEnJB6QSYvGFhIf8CpPIKCBEQhXOQ9YQQkMQMe/JI8SKYAKAYqaUBPUigSgbYRtpQIBGdSArtEJWwEMwICg/m" +
                                 "V9k5AhAFiwgRs0Vx4ieMEJBACA0W3WLiSwQAvSG5n+D+ygASGQAAkQKhsGI+IHNWNBtnpwPLjEkAFcowRh2wICEqggBRP" +
                                 "QKlNeMIEE6IAGCEBuWwTQWEVMAAYs+NgPRpDYe4BwBySgAHwTkbfLwIAAMvBBBRagYoh8YAA+EEGM/aMDSXxABjh4KxAO" +
                                 "wJ0XkhEF5JRE2lIDAgRQQAYOIMcCjAkNHKaAIh2qHiQqgAAAFQKD9PApIQaAAeJFoj/+QQADFGCDBJTjHM24QA4kIF3/2" +
                                 "PERlUPdIFw7D3oVogQssCEjHkDh+yCABRYowArY7IsDhGCGKwIrIwLgAb22dB4TG2UMeGvO7uYIBCwIwASuG6IM9BhIz1" +
                                 "XEBnSQyBf+WBUArDzEBzyg50a8gMBSCuRMd7EBGcgYPadsxA98kNF01QMG076aDzSdiNWNCQY2IPUskNallT1iAMolxA" +
                                 "ZGFrCBZaDGqotqmmTA2nUfEUgwUOkiHjCCGoduHg9MxAkGuYgPcPZNIrCwK96Xpsc44gRwjKk9OIDvHzoitW8KAL5bse03" +
                                 "YYCdicD3lQ2CX0WMfBCcexMCBL7uRl8O5ZN4gK3pAYFwT+LVb0p4LXhacS1T4gQTrOAlArqnodLiAb/uksUxMQCEIA4T" +
                                 "Ma8TzWEBqDFN/RKxO4gIXs4Ihx8Kt7JgTJ0QwPBKZL0eFJB42alTp1++4uFvCjhIEVgPBBj+3coyOJTQZbHeLvUcEx/o" +
                                 "NADszKtjp0kDZD+F4o1E1kqoyCJxusQL+J6m58ziAYfysyWKjBAJaf7fXSIBr0Mx70ChXcQX0V0lHmBzJiGgyagYwaHsHom32yM+lwD0oYaMioOnafCU+K89/zcJ3x7K56tA4Z5kxqvSAiDYkCB9oFTdCo3viQerH8QBag+AzFdC+oEyXyzwXicS9LsRhRmJgQE6qfnDwvFj8pglQIUQCeBeEeT1JhIwC6j2JvEGCRXlE+q2CAX4JiQgC63SKEonCXSHEJVSCYzSKCAQfp2wAZPHJLznCPBDEm0HCSsQKjinCiNAfkaiAJF3MvuEENz+5whFoinYlwo1eChxV1n2hRBORQlAdygZ4wpmVSf5QQkrCBNVRgnQVn+x0EKB0myQYHwIwQMvSAgPYH1XEmuvgH9vImeREHYkgQGrh2eaggNX+Ak/sHOBgiGTQHQksYCHwH+HcjOvcAExWCeYMwn6BhN/xwi+Fyjf8goWEyoHyAjKhxA3WG4ntSevpwoSFCoh2BRseBGPyAj38irIpwro1yiC82dRdxGW9gh02Cg88ApBeCgQ8H+EAC88sYSQAHKhggNyGAqBhypSyAgTwILzcIrLVYmNAgMleAofUHiHAouOEIkxQQCRIEm/InqqcACheCg0AgmUBhOrCAlFqCn+YJgKhfgqM4gII1YSKahsidgoMtAKYmUrCoCAPQEC2cYIK9CDmrKDqzCOk4IC5IYIUEgSE6gIXPIrMPCHpCCLqCJpitCHMTGEikAt5AKNxHiOhyIBkdeEMdGNpaOFezKKp0Awz4ICzIcIm3cU9qcIpYgqFKkKHmgtl2gID2CMF7E2i2Bs6+J+qtAD5OJ5m9SAF/GJi7ADvLgnIDCMo9CJrwIBh0gIJ8CTFkEDjaB2AKMK/fgqBjByB5CHF0GGDQeMtrKJo0CT5OJ3i+AvR1GNioCT+SJ7pXADTBkqraYImdgTCBCShaCQ5IIBxMcJQCkveAmXH2gPelc6f6kpW5f+JxFjfhUTlAlVeYYAlfnyaKWAj79CAFo1A9cWYiLZlrZyWqbwAwZJLv84CFWHFLk4CDkYMRzAiojHedbCAFWje0gxgyMZMfIAA7WICTegkTaICKN5FPVWCNxCm/NQmJ9wgsKZmofQmz0BmTpzi8IJAL8pCqeZL1RxCLB5FJc4AIoZKlc3Ct5Hm0JmCNfZE+qnM3ZJmwNYCs4pnD+4Z1HRnoOgW88pD8xYCueZLxjAfOPJExxZOPM5DxRgClSYLxbQRMrJE90pQv9ZR6awFv+5Gi5zbQAweLM5n0vDgZLwAj4wmKjihoJgV/xJCNconCzQAmkIUgMgAhIqlHr2jT3+MSgnMI3kAgICUAK36Ql4pADbGSgEQBsLQI8jYX6pSC404AIpFgsOFgI7RKCC4IpHYUfOuC4wIAAhMANypwovUEQKwKFbgxZXiRSJ4p82IwIB0AE3gKGo8AEjMBA4sKNGQgEXUBxIQTxiqIEQgAEi4AAlQFLG8AEbUAMBoAAkgAJu2iE2cAFc6VwLoJsdAgHeIAIWYGg7cKK/4GAjUAA+oAIsQABTdm4N0IhT0QABSCIg4A00IAMWEAM1MAI7gKbG8AMnsAIjUAIRYAIywAKDWqi5gatHAQH3+R4wgAEkAA464AMZUAMTsAIX4Krp8AKUsQADUAIx4AE6IAE8QAPYOACkC2pUwsoAPCADOuACEZADLZCsO3ADqlkbH/ACN3ABB7ADE9ABJZACIeACJqAC1koDJIADMLCiAIcCJEADHCAC4GoB4poCFVCuG3AAJ3AD6EqpNvID67quD+CwJ7ABMzAAHdAABZABCRAC9eoAJmACGqACMiABIsABHMACNNCyFIADMBuzMIsCMFCzKCCzMksBLSuwKisCEiABKqABBmACDuACPhACCZACOVABDdADIzADG9CwD3AOLzCxEAtDWJu1Wru1XNu1Xvu1YBu2Yju2ZFu20hIIADs=";


        [HttpGet]
        // GET: Employee
        public ActionResult Index()
        {
            if (Session["USER"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                _context = new DbContext();
                var dir = Server.MapPath("~/App_Data/Images/Employees/");

                String SearchKey = Request["SearchString"];
                allEmployees = _context.Employees.ToList();

                if (!String.IsNullOrEmpty(SearchKey))
                {
                    allEmployees = _context.Employees.Where(s => s.Name.Contains(SearchKey)
                                           || s.Email.Contains(SearchKey) || s.Phone.Contains(SearchKey)).ToList();
                }

                List<Employee> employeesList = new List<Employee>();
                foreach (var employees in allEmployees)
                {
                    if (string.IsNullOrEmpty(employees.Photo))
                    {
                        image = avater;
                    }
                    else
                    {
                        if (System.IO.File.Exists(dir + employees.Photo))
                        {
                            bytes = System.IO.File.ReadAllBytes(dir + employees.Photo);
                            image = Convert.ToBase64String(bytes);
                        }
                        else
                        {
                            image = avater;
                        }
                    }

                    Employee c = new Employee()
                    {
                        UserId = employees.UserId,
                        Name = employees.Name,
                        Email = employees.Email,
                        Phone = employees.Phone,
                        Gender = employees.Gender,
                        Address = employees.Address,
                        Photo = image,
                        Birthdate = employees.Birthdate,
                        NationalId = employees.NationalId,
                        PassportNo = employees.PassportNo,
                        Role = employees.Role
                    };
                    employeesList.Add(c);

                }
                return View(employeesList);
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context = new DbContext();
                    var dir = Server.MapPath("~/App_Data/Images/Employees/");
                    if (Request.Files["photoUpload"] != null)
                    {
                        photo = Request.Files["photoUpload"];
                    }


                    Login login = new Login()
                    {
                        Username = Request["Username"],
                        Password = Request["Password"],
                        LastLoginTime = DateTime.Now
                    };

                    if (photo == null)
                    {
                        employee.Photo = null;

                    }
                    else
                    {
                        var newPhoto = dir;
                        if (!Directory.Exists(newPhoto))
                        {
                            Directory.CreateDirectory(newPhoto);
                        }

                        newPhoto += "\\" + login.Username + System.DateTime.Now.ToString("ddMMyyhhmmss") + photo.FileName;
                        employee.Photo = login.Username + System.DateTime.Now.ToString("ddMMyyhhmmss") + photo.FileName;
                        photo.SaveAs(newPhoto);
                    }
                    employee.Login = login;
                    //_context.Logins.Add(login);
                    _context.Employees.Add(employee);
                    _context.SaveChanges();
                    TempData["Message"] = "Employee Added Successfully. at " + DateTime.Now.ToShortTimeString();
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
                    return View(employee);
                }
            }
            else
            {
                return View(employee);
            }
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            _context = new DbContext();
            var dir = Server.MapPath("~/App_Data/Images/Employees/");

            Employee employee = _context.Employees.SingleOrDefault(d => d.UserId == id);
            if (employee.Photo == null || employee.Photo == "")
            {
                employee.Photo = avater;
            }
            else
            {
                if (System.IO.File.Exists(dir + employee.Photo))
                {
                    bytes = System.IO.File.ReadAllBytes(dir + employee.Photo);
                    image = Convert.ToBase64String(bytes);
                }
                else
                {
                    image = "";
                }
                employee.Photo = image;
            }

            return View(employee);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            _context = new DbContext();
            var dir = Server.MapPath("~/App_Data/Images/Employees/");

            Employee employee = _context.Employees.SingleOrDefault(d => d.UserId == id);
            if (employee.Photo == null || employee.Photo == "")
            {
                employee.Photo = avater;
            }
            else
            {
                if (System.IO.File.Exists(dir + employee.Photo))
                {
                    bytes = System.IO.File.ReadAllBytes(dir + employee.Photo);
                    image = Convert.ToBase64String(bytes);
                }
                else
                {
                    image = "";
                }
                employee.Photo = image;
            }

            return View(employee);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult Delete_Post(int id)
        {
            _context = new DbContext();
            Employee employee = _context.Employees.SingleOrDefault(d => d.UserId == id);
            var login = _context.Logins.SingleOrDefault(d => d.UserId == id);
            _context.Employees.Remove(employee);
            _context.Logins.Remove(login);
            _context.SaveChanges();
            TempData["Message"] = "Successfully Deleted. at " + DateTime.Now.ToShortTimeString();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            _context = new DbContext();
            var dir = Server.MapPath("~/App_Data/Images/Employees/");

            Employee employee = _context.Employees.SingleOrDefault(d => d.UserId == id);
            if (employee.Photo == null || employee.Photo == "")
            {
                employee.Photo = avater;
            }
            else
            {
                if (System.IO.File.Exists(dir + employee.Photo))
                {
                    bytes = System.IO.File.ReadAllBytes(dir + employee.Photo);
                    image = Convert.ToBase64String(bytes);
                }
                else
                {
                    image = "";
                }
                
                employee.Photo = image;
            }

            return View(employee);
        }


        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context = new DbContext();
                    Employee oldEmp = _context.Employees.SingleOrDefault(d => d.UserId == employee.UserId);
                    if(oldEmp !=null ){ oldEmp.Login = _context.Logins.SingleOrDefault(d => d.UserId == oldEmp.UserId);}
                    var dir = Server.MapPath("~/App_Data/Images/Employees/");
                    var httpPostedFileBase = Request.Files["photoUpload"];
                    if (httpPostedFileBase != null && (httpPostedFileBase.FileName != null || httpPostedFileBase.FileName != ""))
                    {
                        photo = Request.Files["photoUpload"];
                    }

                    if (photo.FileName == null)
                    {
                        employee.Photo = "";

                    }
                    else
                    {
                        var newPhoto = dir;
                        if (!Directory.Exists(newPhoto))
                        {
                            Directory.CreateDirectory(newPhoto);
                        }

                        newPhoto += "\\" + oldEmp.Login.Username + System.DateTime.Now.ToString("ddMMyyhhmmss") + photo.FileName;
                        employee.Photo = oldEmp.Login.Username + System.DateTime.Now.ToString("ddMMyyhhmmss") + photo.FileName;
                        photo.SaveAs(newPhoto);
                    }
                    oldEmp.Name = employee.Name;
                    oldEmp.Address = employee.Address;
                    oldEmp.Gender = employee.Gender;
                    oldEmp.Email = employee.Email;
                    oldEmp.Phone = employee.Phone;
                    oldEmp.Photo = employee.Photo;
                    oldEmp.Birthdate = employee.Birthdate;
                    oldEmp.NationalId = employee.NationalId;
                    oldEmp.PassportNo = employee.PassportNo;
                    oldEmp.Role = employee.Role;

                    _context.SaveChanges();
                    TempData["Message"] = "Employee Info Updated Successfully. at " + DateTime.Now.ToShortTimeString();
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
                    return View(employee);
                }
            }
            else
            {
                return View(employee);
            }
        }

        [HttpGet]
        public ActionResult EmployeeDetails()
        {
            if (Session["USER"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Login logger = (Login)Session["USER"];
                _context = new DbContext();
                var employeeDetails = _context.Employees.Where(m => m.UserId == logger.UserId);
                Employee employee;
                if (employeeDetails.Any())
                {
                    var dir = Server.MapPath("~/App_Data/Images/Employees/");


                    employee = employeeDetails.First();
                    if (employee.Photo != null || employee.Photo !="")
                    {
                        if (System.IO.File.Exists(dir + employee.Photo))
                        {
                            bytes = System.IO.File.ReadAllBytes(dir + employee.Photo);
                            image = Convert.ToBase64String(bytes);
                        }
                        else
                        {
                            image = "";
                        }
                        employee.Photo = image;
                    }
                    else
                    {
                        employee.Photo = avater;
                    }
                }
                else
                {
                    employee = new Employee()
                    {
                        UserId = 0,
                        Birthdate = DateTime.Today.AddYears(-35),
                        Role = "Employee"
                    };
                }
                return View(employee);
            }
        }

        [HttpPost]
        public ActionResult EmployeeDetails(Employee employee)
        {
            if (Session["USER"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Login logger = (Login)Session["USER"];
                photo = Request.Files["photoUpload"];


                var dir = Server.MapPath("~/App_Data/Images/Employees/");



                if (ModelState.IsValid)
                {

                    employee.UserId = logger.UserId;

                    _context = new DbContext();

                    var employeeDetails = _context.Employees.Where(m => m.UserId == logger.UserId);

                    if (employeeDetails.Any())
                    {
                        var md = employeeDetails.First();

                        md.Name = employee.Name;
                        md.Address = employee.Address;
                        md.Gender = employee.Gender;
                        md.Email = employee.Email;
                        md.Phone = employee.Phone;
                        md.Birthdate = employee.Birthdate;
                        md.NationalId = employee.NationalId;
                        md.PassportNo = employee.PassportNo;


                        if (photo.FileName != "")
                        {
                            var newPhoto = dir;
                            if (!Directory.Exists(newPhoto))
                            {
                                Directory.CreateDirectory(newPhoto);
                            }
                            newPhoto += "\\" + logger.Username + System.DateTime.Now.ToString("ddMMyyhhmmss") +
                                        photo.FileName;
                            md.Photo = logger.Username + System.DateTime.Now.ToString("ddMMyyhhmmss") + photo.FileName;
                            photo.SaveAs(newPhoto);
                        }
                    }
                    else
                    {
                        TempData["Message"] = "Some thing wrong!";
                    }
                }



                try
                    {
                        _context.SaveChanges();

                        employee = _context.Employees.FirstOrDefault(x => x.UserId == logger.UserId);
                        TempData["Message"] = "Update Success. at " + DateTime.Now.ToShortTimeString();
                        return RedirectToAction("EmployeeDetails", "Employee");
                    }
                    catch (Exception ex)
                    {

                    }
                }
                return View(employee);
        }
    }
}