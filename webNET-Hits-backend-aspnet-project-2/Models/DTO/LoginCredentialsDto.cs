using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace webNET_Hits_backend_aspnet_project_2.Models.DTO;

public class LoginCredentials
{
    [NotNull]
    [EmailAddress]
    [MinLength(1)]
    public string Email { get; set; }
    
    [NotNull]
    [MinLength(1)]
    public string Password { get; set; }
}