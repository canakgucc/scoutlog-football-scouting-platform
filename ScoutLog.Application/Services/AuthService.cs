using ScoutLog.Application.DTOs.Auth;
using ScoutLog.Application.Interfaces.Repositories;
using ScoutLog.Application.Interfaces.Security;
using ScoutLog.Application.Interfaces.Services;
using ScoutLog.Domain.Entities;

namespace ScoutLog.Application.Services;

public class AuthService(
    IUserRepository userRepository,
    IRepository<Role> roleRepository,
    IRepository<Club> clubRepository,
    IPasswordHasher passwordHasher,
    IJwtTokenGenerator jwtTokenGenerator) : IAuthService
{
    public async Task<AuthResponseDto> LoginAsync(
        LoginRequestDto request,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
        {
            throw new ArgumentException("Email and password are required.");
        }

        var user = await userRepository.GetByEmailAsync(request.Email.Trim(), cancellationToken);
        if (user is null || !user.IsActive || !passwordHasher.Verify(request.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        return CreateAuthResponse(user);
    }

    public async Task<AuthResponseDto> RegisterAsync(
        RegisterRequestDto request,
        CancellationToken cancellationToken = default)
    {
        ValidateRegisterRequest(request);

        var normalizedEmail = request.Email.Trim().ToLowerInvariant();
        var existingUser = await userRepository.GetByEmailAsync(normalizedEmail, cancellationToken);
        if (existingUser is not null)
        {
            throw new InvalidOperationException("A user with this email already exists.");
        }

        var role = await roleRepository.GetByIdAsync(request.RoleId, cancellationToken);
        if (role is null)
        {
            throw new InvalidOperationException($"Role with id {request.RoleId} does not exist.");
        }

        if (!request.ClubId.HasValue)
        {
            throw new ArgumentException("Club id is required.");
        }

        var club = await clubRepository.GetByIdAsync(request.ClubId.Value, cancellationToken);
        if (club is null)
        {
            throw new InvalidOperationException($"Club with id {request.ClubId.Value} does not exist.");
        }

        var user = new User
        {
            FullName = request.FullName.Trim(),
            Email = normalizedEmail,
            PasswordHash = passwordHasher.Hash(request.Password),
            RoleId = request.RoleId,
            Role = role,
            ClubId = club.Id,
            Club = club,
            TeamId = request.TeamId,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        await userRepository.AddAsync(user, cancellationToken);
        await userRepository.SaveChangesAsync(cancellationToken);

        return CreateAuthResponse(user);
    }

    private AuthResponseDto CreateAuthResponse(User user)
    {
        if (!user.ClubId.HasValue || user.Club is null)
        {
            throw new InvalidOperationException("User must be assigned to a club.");
        }

        var token = jwtTokenGenerator.GenerateToken(user);

        return new AuthResponseDto(
            token.AccessToken,
            token.ExpiresAt,
            user.Id,
            user.FullName,
            user.Email,
            user.Role.Name,
            user.ClubId.Value,
            user.Club.Name);
    }

    private static void ValidateRegisterRequest(RegisterRequestDto request)
    {
        if (string.IsNullOrWhiteSpace(request.FullName)
            || string.IsNullOrWhiteSpace(request.Email)
            || string.IsNullOrWhiteSpace(request.Password))
        {
            throw new ArgumentException("Full name, email and password are required.");
        }

        if (request.Password.Length < 6)
        {
            throw new ArgumentException("Password must be at least 6 characters.");
        }

        if (request.RoleId <= 0)
        {
            throw new ArgumentException("Role id must be greater than zero.");
        }
    }
}
