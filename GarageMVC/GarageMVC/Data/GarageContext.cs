using GarageMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace GarageMVC.Data
{
    public class GarageContext(DbContextOptions options) : DbContext(options), IGarageContext
    {
        public DbSet<ParkedVehicleModel> ParkedVehicles => Set<ParkedVehicleModel>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
