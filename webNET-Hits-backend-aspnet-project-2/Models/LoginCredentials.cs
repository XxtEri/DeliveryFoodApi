using System.ComponentModel.DataAnnotations;

namespace webNET_Hits_backend_aspnet_project_2.Models;

public class LoginCredentials
{
    [EmailAddress]
    [MinLength(1)]
    [Required]
    public string Email { get; set; }
    
    [MinLength(1)]
    [Required]
    public string Password { get; set; }
}