using System.Diagnostics.CodeAnalysis;

namespace webNET_Hits_backend_aspnet_project_2.Models;

public class Response
{
    [MaybeNull]
    public string Status { get; set; }
    
    [MaybeNull]
    public string Message { get; set; }
}