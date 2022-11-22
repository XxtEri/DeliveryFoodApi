using Microsoft.EntityFrameworkCore;
using webNET_Hits_backend_aspnet_project_2.Models.DTO;

namespace webNET_Hits_backend_aspnet_project_2.Models;

public class ApplicationDbContext: DbContext
{
    public DbSet<DishBasketDto> DishBaskets { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
        Database.EnsureCreated();
    }

    protected void OnDishBasketModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DishBasketDto>().HasKey(x => x.Id);
    }
}