using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMS_Data.Entities
{
    public class Login
    {
        [Key]
        public int UserId { get; set; }

        [MaxLength(8, ErrorMessage = "Username Minimum 8 Character."), Required(ErrorMessage = "Usernmae Required.")]
        public string Username { get; set; }

        [MaxLength(8, ErrorMessage = "Password Minimum 8 Character."), Required(ErrorMessage = "Password Required.")]
        public string Password { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime LastLoginTime { get; set; }

        public Customer Customer { get; set; }

        public Employee Employee { get; set; }
    }
}
