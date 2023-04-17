using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalProject.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, String>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<ItemImages> ItemImages { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => new { oi.OrderID, oi.ItemID });

            modelBuilder.Entity<OrderStatus>()
            .HasKey(os => os.OrderID);

            modelBuilder.Entity<OrderStatus>()
                .HasOne(os => os.Order)
                .WithOne(o => o.OrderStatus)
                .HasForeignKey<OrderStatus>(os => os.OrderID);
            
            modelBuilder.Entity<ItemImages>()
            .HasOne(i => i.Item)
            .WithMany(i => i.ItemImages)
            .HasForeignKey(i => i.ItemID);
        }

    }
}
