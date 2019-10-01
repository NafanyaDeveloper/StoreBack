using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Store.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.CORE.EF
{
    public class StoreContext: IdentityDbContext<Customer, IdentityRole<Guid>, Guid>
    {
        public StoreContext(DbContextOptions<StoreContext> opt): base(opt)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole<Guid>>().HasData(
                new IdentityRole<Guid>[]
                {
                    new IdentityRole<Guid>
                    {
                        Id = Guid.NewGuid(),
                        Name = "customer",
                        NormalizedName = "CUSTOMER"
                    },
                    new IdentityRole<Guid>
                    {
                        Id = Guid.NewGuid(),
                        Name = "manager",
                        NormalizedName = "MANAGER"
                    }
                });

            base.OnModelCreating(modelBuilder);
           /* modelBuilder.Entity<Customer>()
                .HasOne<User>(c => c.User)
                .WithMany(u => u.Customers)
                .OnDelete(DeleteBehavior.SetNull);*/
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<User> StoreUsers { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
