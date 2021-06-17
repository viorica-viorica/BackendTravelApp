using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTravelApp.Models
{
    public class TravelContext : DbContext
    {
        public TravelContext(DbContextOptions<TravelContext> options) : base(options)
        {

        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Hotel> Hotel { get; set; }
        public DbSet<Restaurant> Restaurant { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<TouristicObjective> TouristicObjective { get; set; }
    }
}
