namespace ScoutLog.Application.Interfaces.Security;

public record AuthToken(string AccessToken, DateTime ExpiresAt);
