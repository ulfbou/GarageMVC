using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GarageMVC.Models;

namespace GarageMVC.Data
{
    public class CarContext : DbContext
    {
        public CarContext (DbContextOptions<CarContext> options)
            : base(options)
        {
        }

        public DbSet<GarageMVC.Models.ParkedVehicleModel> ParkedVehicleModel { get; set; } = default!;
    }
}
