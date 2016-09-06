using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRMS_Web_Clinte.Models
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public bool HasUsername { get; set; }
    }

    public class DashboardViewModel
    {
        public int TotalEmployee { get; set; }
        public int TotalVaicles { get; set; }

        public int TotalCustomers { get; set; }

        public int TotalOngingRes { get; set; }

        public int TotalPendingRes { get; set; }
    }

    public class AllCarReport
    {
        public int VehicleId { get; set; }
        public string VehicleName { get; set; }

        public string Photo { get; set; }

        public int TotalReserved { get; set; }

        public double TotalEarn { get; set; }
    }

    public class CarReport
    {
        public int VehicleId { get; set; }
        public string VehicleName { get; set; }

        public string Photo { get; set; }

        public DateTime ReservedDate { get; set; }

        public string ReservedBy { get; set; }

        public double ReservationEarn { get; set; }

        public double GrandTotal { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}