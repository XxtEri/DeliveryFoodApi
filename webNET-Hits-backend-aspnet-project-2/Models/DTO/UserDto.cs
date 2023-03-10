using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using webNET_Hits_backend_aspnet_project_2.Enums;

namespace webNET_Hits_backend_aspnet_project_2.Models.DTO;

public class UserDto
{
    public Guid Id { get; set; }
    
    [MinLength(1)]
    [Required]
    public string FullName { get; set; }
    
    [DataType(DataType.Date)]
    [MaybeNull]
    public string BirthDate { get; set; }
    
    [Required]
    public Gender Gender { get; set; }
    
    [MaybeNull]
    public string Address { get; set; }
    
    [EmailAddress]
    [MaybeNull]
    public string Email { get; set; }
    
    [Phone]
    [MaybeNull]
    public string PhoneNumber { get; set; }
}