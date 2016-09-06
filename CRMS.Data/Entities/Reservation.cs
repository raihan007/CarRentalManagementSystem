using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMS_Data.Entities
{

    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }


        [Required(ErrorMessage = "Reservation Date is Required.")]
        public DateTime ReservationDate { get; set; }


        [Required(ErrorMessage = "Customer ID is Required.")]
        public int ReservedBy { get; set; }


        [Required(ErrorMessage = "Pick-Up Location is Required.")]
        [StringLength(50)]
        public string PickUpLocation { get; set; }


        [Required(ErrorMessage = "Pick-Up Date is Required.")]
        [Column(TypeName = "date")]
        public DateTime PickUpDate { get; set; }


        [Required(ErrorMessage = "Pick-Up Time is Required.")]
        public String PickUpTime { get; set; }


        [Required(ErrorMessage = "Drop-Off Location is Required.")]
        [StringLength(50)]
        public string DropOffLocation { get; set; }


        [Required(ErrorMessage = "Drop-Off Date is Required.")]
        [Column(TypeName = "date")]
        public DateTime DropOffDate { get; set; }


        [Required(ErrorMessage = "Drop-Off Time is Required.")]
        public String DropOffTime { get; set; }


        [Required(ErrorMessage = "Reservation Amount is Required.")]
        public double ReservationAmount { get; set; }


        [Required(ErrorMessage = "Reservation Status is Required.")]
        [StringLength(15)]
        public string ReservationStatus { get; set; }


        [Required(ErrorMessage = "Reservation Vehicle Id is Required.")]
        public int ReservationVehicleId { get; set; }


        public virtual ICollection<Bill> Bills { get; set; }


        [ForeignKey("ReservedBy")]
        public virtual Customer Customer { get; set; }


        [ForeignKey("ReservationVehicleId")]
        public virtual Vehicle Vehicle { get; set; }
    }
}
