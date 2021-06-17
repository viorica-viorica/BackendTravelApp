using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTravelApp.Models
{
    public class UpdateUser
    {
        public int UserId { get; set; }
        public string FirstLastName { get; set; }
        public string  PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
