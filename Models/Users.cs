using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTravelApp.Models
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }

        [Column(TypeName="varchar(50)")]
        public string FirstLastName { get; set; }

        [Column(TypeName="varchar(10)")]
        public string PhoneNumber { get; set; }

        [Column(TypeName="varchar(50)")]
        public string Email { get; set; }

        [Column(TypeName="varchar(20)")]
        public string Username { get; set; }

        [Column(TypeName="varchar(20)")]
        public string Password { get; set; }

        [Column(TypeName="varbinary(MAX)")]
        public byte[] ProfilePhoto { get; set; }

        [Column(TypeName="float")]
        public float Latitude { get; set; }

        [Column(TypeName="float")]
        public float Longitude { get; set; }

        [Column(TypeName="varchar(6)")]
        public string Role { get; set; }
    }
}
