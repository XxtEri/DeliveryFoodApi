using System.Diagnostics.CodeAnalysis;

namespace webNET_Hits_backend_aspnet_project_2.Models;

public class TokenResponse
{
    [MaybeNull]
    public string Token { get; set; }
}