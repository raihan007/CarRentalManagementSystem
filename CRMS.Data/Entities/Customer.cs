using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMS_Data.Entities
{
    public class Customer
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Name Required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address Required.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Gender Required.")]
        public string Gender { get; set; }

        //[Required(ErrorMessage = "Phone Required.")]
        public string Phone { get; set; }

        [EmailAddress(ErrorMessage = "Please enter valid email address."), Required(ErrorMessage = "Email Required.")]
        public string Email { get; set; }
        
        public string Photo { get; set; }

        [Required(ErrorMessage = "Enter your date of birth.")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime Birthdate { get; set; }

        [Required(ErrorMessage = "National Id Required.")]
        public string NationalId { get; set; }

        [Required(ErrorMessage = "Passport No Required.")]
        public string PassportNo { get; set; }
        
        [ForeignKey("UserId")]
        public Login Login { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
