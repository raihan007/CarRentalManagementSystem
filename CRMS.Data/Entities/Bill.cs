using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMS_Data.Entities
{
    public class Bill
    {
        [Key]
        public int BillId { get; set; }

        [Required(ErrorMessage = "Bill Amount is Required.")]
        public double BillAmount { get; set; }

        [Required(ErrorMessage = "Bill Date is Required.")]
        public DateTime BillDate { get; set; }

        public double Tax { get; set; }

        public double Discount { get; set; }

        [Required(ErrorMessage = "Total Amount is Required.")]
        public double TotalAmount { get; set; }

        [Required(ErrorMessage = "Reservation ID is Required.")]
        public int ReservationId { get; set; }

        [Required]
        [StringLength(10)]
        public string BillStatus { get; set; }

        [Required]
        public int BillCreated { get; set; }

        [ForeignKey("BillCreated")]
        public virtual Employee Employee { get; set; }

        [ForeignKey("ReservationId")]
        public virtual Reservation Reservation { get; set; }
    }
}
