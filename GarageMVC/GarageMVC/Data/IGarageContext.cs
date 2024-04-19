using Microsoft.EntityFrameworkCore;
using GarageMVC.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GarageMVC.Data
{
    public interface IGarageContext
    {
        DbSet<ParkedVehicleModel> ParkedVehicles { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        EntityEntry Update(object parkedVehicleModel);
    }
}