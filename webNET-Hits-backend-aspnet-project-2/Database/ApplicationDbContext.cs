using Microsoft.EntityFrameworkCore;

namespace webNET_Hits_backend_aspnet_project_2.Models;

public class ApplicationDbContext: DbContext
{
    public DbSet<DishBasket> DishBaskets { get; set; }
    public DbSet<Dish> Dishes { get; set; }
    public DbSet<Response> Responses { get; set; }
    public DbSet<User> Users { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasOne(o => o.DishBasket)
            .WithOne().HasForeignKey<User>().IsRequired();
    }
}