using System;
using SET_Management;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SET_Management.Models.Entity;

namespace SET_Management.Helpers.DbContexts
{
    public class dbContext : DbContext
    {
        public dbContext(DbContextOptions<dbContext> options) : base(options) { }
        public DbSet<mstUser> mstUsers { get; set; }
        public DbSet<mstVehicle> mstVehicle { get; set; }
        public DbSet<mstCompany> mstCompany { get; set; }

        /*protected override void OnModelCreating(ModelBuilder builder)
        {
            //base.OnModelCreating(builder);
            builder.Entity<mstUser>().ToTable("mstUser");
            builder.Entity<mstVehicle>().ToTable("mstVehicle");
            builder.Entity<mstCompany>().ToTable("mstCompany");

        }*/
    }
}
