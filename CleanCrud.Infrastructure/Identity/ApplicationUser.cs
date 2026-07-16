using Microsoft.AspNetCore.Identity;

namespace CleanCrud.Infrastructure.Identity;

public sealed class ApplicationUser : IdentityUser
{
    public string? FullName { get; set; }
}
