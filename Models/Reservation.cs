using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTravelApp.Models
{
    public class Reservation
    {
        [Column(TypeName="int")]
        public int ReservationId { get; set; }

        [Column(TypeName="varchar(10)")]
        public string Username { get; set; }

        [Column(TypeName="varchar(50)")]
        public string ReservationName { get; set; }

        [Column(TypeName="varchar(10)")]
        public string StartDate { get; set; }

        [Column(TypeName= "varchar(10)")]
        public string EndDate { get; set; }

        [Column(TypeName="varchar(8)")]
        public string Time { get; set; }

        [Column(TypeName="float")]
        public float Price { get; set; }
    }
}
