using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTravelApp.Models
{
    public class Hotel
    {
        [Key]
        public int HotelId { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Address { get; set; }

        [Column(TypeName = "float")]
        public float Price { get; set; }

        [Column(TypeName = "int")]
        public int Rooms { get; set; }

        [Column(TypeName = "int")]
        public int Adults { get; set; }

        [Column(TypeName = "int")]
        public int Children { get; set; }

        [Column(TypeName = "float")]
        public float Latitude { get; set; }

        [Column(TypeName = "float")]
        public float Longitude { get; set; }

        [Column(TypeName = "varbinary(MAX)")]
        public byte[] Photo { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string HotelUrl { get; set; }
    }
}
