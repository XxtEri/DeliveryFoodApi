using Microsoft.EntityFrameworkCore;

namespace webNET_Hits_backend_aspnet_project_2.Models;

public class ApplicationDbContext: DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Dish> Dishes { get; set; }
    public DbSet<DishBasket> BasketDishes { get; set; }
    public DbSet<OrderingDish> OrderingDishes { get; set; }
    public DbSet<Order> Orders { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //TODO: cделать unique и ключи

        modelBuilder.Entity<DishBasket>()
            .HasOne(p => p.User)
            .WithMany(c => c.DishBasket);
        
        modelBuilder.Entity<Order>()
            .HasOne(p => p.User)
            .WithMany(c => c.Order);
        
        modelBuilder.Entity<DishBasket>()
            .HasOne(p => p.Dish);
    }
}