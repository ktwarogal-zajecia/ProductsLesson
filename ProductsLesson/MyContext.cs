using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsLesson
{
    public class MyContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<PropertyDefinition> PropertyDefinitions { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<PropertyValue>()
                .HasOne(p => p.Product)
                .WithMany(p => p.Properties)
                .HasForeignKey(p => p.ProductId);

            modelBuilder.Entity<PropertyValue>()
                .HasOne(p => p.PropertyDefinition)
                .WithMany(p => p.Values)
                .HasForeignKey(p => p.PropertyDefinitionId);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "server=127.0.0.1;uid=root;pwd=;database=ProductsLesson";
            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion("10.4.28"));
            base.OnConfiguring(optionsBuilder);
        }
    }
}
