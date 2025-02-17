using SET_Management;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SET_Management.Models;
using SET_Management.Helpers.DbContexts;
using SET_Management.Helpers.Middlewares;
using SET_Management.Interface;
using SET_Management.Repositories;
using System;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Connection string for your database
        string connectionString = @"server=localhost;user=root;password=Noone1903;database=TransportMS_V01;";

        builder.Services.AddDbContext<dbContext>(option => option.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 3, 0))));

         // string connectionString = @"Server=172.16.15.17;Database=testDB;user=sa;password=Rndsoft@123";

        // Register your dbContext with the dependency injection container
        //builder.Services.AddDbContext<dbContext>(options =>options.UseSqlServer(connectionString));

        builder.Services.AddSingleton<ApiResponseRepository>();
        builder.Services.AddScoped<authRepository>();

        // Register IApiResponseRepository and its implementation
        builder.Services.AddScoped<IApiResponseRepository, ApiResponseRepository>();
        builder.Services.AddScoped<IauthRepository, authRepository>();


        // Register your middleware
        builder.Services.AddScoped<globalExceptionHandlingMiddleware>();

        // Add services to the container
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // Configure the HTTP request pipeline
        if (!app.Environment.IsDevelopment())
        {
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        // Use the middleware
        app.UseMiddleware<globalExceptionHandlingMiddleware>();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
