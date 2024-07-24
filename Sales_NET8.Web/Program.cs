using Microsoft.EntityFrameworkCore;
using Sales_NET8.Web.Data;

namespace Sales_NET8.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //Add runtime compilation
            builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

            // Configure Entity Framework and the database context
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(connectionString));


            builder.Services.AddTransient<SeedDb>();
            builder.Services.AddScoped<IRepository, Repository>();

            var app = builder.Build();

            // Run seeding
            RunSeeding(app);

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
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }

        private static void RunSeeding(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var seeder = scope.ServiceProvider.GetRequiredService<SeedDb>();
                seeder.SeedAsync().Wait();
            }
        }
    }
}
