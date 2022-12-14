using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using webNET_Hits_backend_aspnet_project_2.Enums;

namespace webNET_Hits_backend_aspnet_project_2.Models;

public class UserRegisterModel
{
    [MinLength(1)]
    [Required]
    public string FullName { get; set; }
    
    [MinLength(6)]
    [DataType(DataType.Password)]
    [Required]
    public string Password { get; set; }
    
    [EmailAddress]
    [Required]
    [MinLength(1)]
    public string Email { get; set; }
    
    [MaybeNull]
    public string Address { get; set; }

    [DataType(DataType.DateTime)]
    [MaybeNull]
    public string BirthDate { get; set; }
    
    [Required]
    public Gender Gender { get; set; }

    [Phone]
    [MaybeNull]
    public string PhoneNumber { get; set; }
}