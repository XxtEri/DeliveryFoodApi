using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webNET_Hits_backend_aspnet_project_2.Models;

public class DisactiveToken
{
    [Key]
    [ForeignKey("User")]
    public Guid Id { get; set; }
    
    public string Token { get; set; }
    
    public User User { get; set; }
}