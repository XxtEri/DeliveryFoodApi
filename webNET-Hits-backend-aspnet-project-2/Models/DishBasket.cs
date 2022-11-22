namespace webNET_Hits_backend_aspnet_project_2.Models;

public class DishBasket
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public double Price { get; set; }
    
    public double TotalPrice { get; set; }
    
    public int Amount { get; set; }
    
    public string? Image { get; set; }
}