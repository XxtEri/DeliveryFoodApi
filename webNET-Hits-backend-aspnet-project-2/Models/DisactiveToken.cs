using System.ComponentModel.DataAnnotations;

namespace webNET_Hits_backend_aspnet_project_2.Models;

public class DisactiveToken
{
    [Key]
    public Guid Id { get; set; }
    
    public string Token { get; set; }
    
    public DateTime TimeLogOut { get; set; }
}