using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webNET_Hits_backend_aspnet_project_2.Models;

public class RatingUser
{
    [Key]
    [ForeignKey("User")]
    public Guid UserId { get; set; }
    [Key]
    [ForeignKey("Dish")]
    public Guid DishId { get; set; }
    
    public int Rating { get; set; }
    
    public User User { get; set; }
    public Dish Dish { get; set; }
}