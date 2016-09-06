using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace CRMS_Web_Clinte.Models
{

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username Required.")]
        [RegularExpression(@"^([a-zA-Z])[a-zA-Z_-]*[\w_-]*[\S]$|^([a-zA-Z])[0-9_-]*[\S]$|^[a-zA-Z]*[\S]$",ErrorMessage = "Rquired!.")]
        [Display(Name = "Username")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Required.")]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }

    public class RegisterCustomerViewModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Name Required.")]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Please enter valid email address."), Required(ErrorMessage = "Email Required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Required.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Address Required.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Gender Required.")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "National Id Required.")]
        public string NationalId { get; set; }

        [Required(ErrorMessage = "Passport No Required.")]
        public string PassportNo { get; set; }

        public String Photo { get; set; }

        [Required(ErrorMessage = "Enter your date of birth.")]
        public DateTime Birthdate { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "The username must be at least 5 characters long.")]
        //[RegularExpression(@"^(?=.{8,20}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$")]
        public string Username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
