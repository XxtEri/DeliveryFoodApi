using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace webNET_Hits_backend_aspnet_project_2.Models.DTO;

public class LoginCredentialsDto
{
    [EmailAddress]
    [MinLength(1)]
    [Required]
    public string Email { get; set; }
    
    [MinLength(1)]
    [Required]
    public string Password { get; set; }
}