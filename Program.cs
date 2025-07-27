using Microsoft.EntityFrameworkCore;
using account_web.Data;
using account_web.Services;

namespace account_web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        // 設定 SQLite 資料庫連接
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=AccountApp.db";
            // 確保資料庫路徑相對於應用程式根目錄
            if (!Path.IsPathRooted(connectionString.Replace("Data Source=", "")))
            {
                var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "AccountApp.db");
                connectionString = $"Data Source={dbPath}";
            }
            options.UseSqlite(connectionString);
        });
        
        builder.Services.AddScoped<UserServices>();
        builder.Services.AddScoped<FactoryServices>();

        var app = builder.Build();

        // 確保資料庫已建立並應用遷移
        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            DatabaseHelper.EnsureDatabaseCreated(context);
        }

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
}
