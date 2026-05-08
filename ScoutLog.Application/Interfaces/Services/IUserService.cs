using ScoutLog.Application.DTOs.Users;

namespace ScoutLog.Application.Interfaces.Services;

public interface IUserService
{
    Task<IReadOnlyList<UserDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<UserDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<UserDto> CreateAsync(CreateUserDto request, CancellationToken cancellationToken = default);
    Task<UserDto?> UpdateAsync(int id, UpdateUserDto request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
