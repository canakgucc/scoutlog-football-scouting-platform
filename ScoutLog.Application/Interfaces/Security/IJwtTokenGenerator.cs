using ScoutLog.Domain.Entities;

namespace ScoutLog.Application.Interfaces.Security;

public interface IJwtTokenGenerator
{
    AuthToken GenerateToken(User user);
}
