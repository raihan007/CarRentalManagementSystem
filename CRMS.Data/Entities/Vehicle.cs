using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRMS_Data.Entities
{
    public class Vehicle
    {
        [Key]
        public int VehicleId { get; set; }

        [Required(ErrorMessage = "Vehicle Name is Required.")]
        [StringLength(50)]
        public string VehicleName { get; set; }

        [Required(ErrorMessage = "Vehicle Brand is Required.")]
        [StringLength(20)]
        public string Brand { get; set; }

        [StringLength(50)]
        public string Photo { get; set; }

        [Required(ErrorMessage = "Vehicle Details is Required.")]
        public string Details { get; set; }

        [Required(ErrorMessage = "Vehicle Capacity is Required.")]
        [StringLength(50)]
        public string Capacity { get; set; }

        [Required(ErrorMessage = "Vehicle Cost Per Hour is Required.")]
        public double CostPerHour { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
