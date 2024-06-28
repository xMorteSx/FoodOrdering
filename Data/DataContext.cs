using FoodOrdering.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodOrdering.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ChatUserMessage> ChatMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasMany(e => e.Comments)
                .WithOne(e => e.OrderProduct)
                .HasForeignKey("ProductId")
                .IsRequired();
        }
    }
}
