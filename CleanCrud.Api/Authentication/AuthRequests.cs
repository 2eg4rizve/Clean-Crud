using System.ComponentModel.DataAnnotations;

namespace CleanCrud.Api.Authentication;

public sealed class RegisterRequest
{
    [Required, StringLength(100)]
    public string FullName { get; init; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; init; } = string.Empty;

    [Required, StringLength(100, MinimumLength = 6)]
    public string Password { get; init; } = string.Empty;
}

public sealed class LoginRequest
{
    [Required, EmailAddress]
    public string Email { get; init; } = string.Empty;

    [Required]
    public string Password { get; init; } = string.Empty;
}

public sealed record AuthResponse(string Token, DateTime ExpiresAtUtc, string Email, IReadOnlyList<string> Roles);
