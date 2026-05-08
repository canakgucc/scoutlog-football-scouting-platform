namespace ScoutLog.Application.Interfaces.Security;

public interface ICurrentUserContext
{
    int UserId { get; }
    int ClubId { get; }
    string Role { get; }
}
