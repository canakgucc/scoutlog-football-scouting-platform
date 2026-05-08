using System.Security.Cryptography;
using System.Text;
using ScoutLog.Application.Interfaces.Security;

namespace ScoutLog.Infrastructure.Security;

public class Sha256PasswordHasher : IPasswordHasher
{
    private const string Prefix = "sha256";

    public string Hash(string password)
    {
        return $"{Prefix}${ComputeSha256(password)}";
    }

    public bool Verify(string password, string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
        {
            return false;
        }

        var expectedHash = Hash(password);
        return CryptographicOperations.FixedTimeEquals(
            Encoding.UTF8.GetBytes(expectedHash),
            Encoding.UTF8.GetBytes(passwordHash));
    }

    private static string ComputeSha256(string value)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(value));
        return Convert.ToHexString(bytes).ToLowerInvariant();
    }
}
