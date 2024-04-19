using GarageMVC.Data;
using Microsoft.EntityFrameworkCore;

//1. Create Link
//2. Create ActionMethod in controller (endpoint)
//3. Get data from DB
//4. Create new ViewModel with data to display
//5. Create View with your new ViewModel (use details template)

namespace GarageMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSingleton<VehicleConstants>();
            builder.Services.AddDbContext<IGarageContext, GarageContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Garage}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
