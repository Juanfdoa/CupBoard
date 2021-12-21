using FoodWarehouse.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodWarehouse
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            base.OnModelCreating(modelBuilder);

        }

        public DbSet<Categories> Categories { get; set; }
        public DbSet<CupBoard> CupBoard { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<status> Status { get; set; }
        
    }
}
