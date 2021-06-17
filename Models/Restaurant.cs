using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTravelApp.Models
{
    public class Restaurant
    {
        [Key]
        public int RestaurantId { get; set; }

        [Column(TypeName="varchar(30)")]
        public string Name { get; set; }

        [Column(TypeName="varchar(50)")]
        public string Address { get; set; }

        [Column(TypeName="varchar(50)")]
        public string Schedule { get; set; }

        [Column(TypeName="float")]
        public float Latitude { get; set; }

        [Column(TypeName="float")]
        public float Longitude { get; set; }

        [Column(TypeName="varbinary(MAX)")]
        public byte[] Photo { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string RestaurantUrl { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string MenuUrl { get; set; }
    }
}
