using GarageMVC.Data;
using Microsoft.EntityFrameworkCore;

namespace GarageMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSingleton<VehicleConstants>();
            if (builder.Configuration["database"] == "inMemory")
                builder.Services.AddDbContext<IGarageContext, GarageContext>(options =>
                options.UseInMemoryDatabase(databaseName: "something"));
            else
                builder.Services.AddDbContext<IGarageContext, GarageContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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
