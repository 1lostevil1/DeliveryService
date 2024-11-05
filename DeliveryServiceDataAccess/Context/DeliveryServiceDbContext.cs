using DeliveryServiceDataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeliveryServiceDataAccess;

public class DeliveryServiceDbContext : DbContext
{
     public DeliveryServiceDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<DeliveryMan> DeliveryMen { get; set; }
    public DbSet<Administrator> Administrators { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<OrderStatus>  Statuses { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(x => x.Id);
        modelBuilder.Entity<Order>().HasKey(x => x.Id);
        modelBuilder.Entity<Product>().HasKey(x => x.Id);
        modelBuilder.Entity<OrderStatus>().HasKey(x => x.Id);
        modelBuilder.Entity<Review>().HasKey(x => x.Id);
        modelBuilder.Entity<OrderProduct>().HasKey(x => x.Id);
        
        modelBuilder.Entity<Order>().HasOne(x => x.User)
            .WithMany(x => x.Orders).HasForeignKey(x => x.UserId);
        modelBuilder.Entity<Order>().HasOne(x => x.OrderStatus)
            .WithMany(x => x.Orders).HasForeignKey(x => x.StatusId );
        
        modelBuilder.Entity<Review>().HasOne(x => x.User)
            .WithMany(x => x.Reviews).HasForeignKey(x => x.UserId);

        modelBuilder.Entity<Order>().HasOne(x => x.DeliveryMan)
            .WithMany(x => x.Orders).HasForeignKey(x => x.UserId);

        modelBuilder.Entity<OrderProduct>().HasOne(x => x.Order)
            .WithMany(x => x.OrderProducts).HasForeignKey(x => x.OrderId);
        modelBuilder.Entity<OrderProduct>().HasOne(x => x.Product)
            .WithMany(x => x.OrderProducts).HasForeignKey(x => x.ProductId);
        
    }
}