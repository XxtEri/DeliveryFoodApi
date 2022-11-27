using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace webNET_Hits_backend_aspnet_project_2.Models;

[Keyless]
public class Response
{
    [MaybeNull]
    public string Status { get; set; }
    
    [MaybeNull]
    public string Message { get; set; }
}