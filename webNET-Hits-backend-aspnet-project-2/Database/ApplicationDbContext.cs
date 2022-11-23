using Microsoft.EntityFrameworkCore;
using webNET_Hits_backend_aspnet_project_2.Models.DTO;

namespace webNET_Hits_backend_aspnet_project_2.Models;

public class ApplicationDbContext: DbContext
{
    public DbSet<DishBasket> DishBaskets { get; set; }
    public DbSet<DishDto> Dishes { get; set; }
    public DbSet<Response> Responses { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
        Database.EnsureCreated();
    }
}