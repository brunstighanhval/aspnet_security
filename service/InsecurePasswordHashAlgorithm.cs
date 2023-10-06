using System.Security.Cryptography;
using System.Text;
using Service;

namespace service;

// Never actually do this in your applications.
public class InsecurePasswordHashAlgorithm : PasswordHashAlgorithm
{
    public const string Name = "md5";
    public override string GetName() => Name;

    // It doesn't even use a salt 😱
    public string GenerateSalt() => string.Empty;

    public override string HashPassword(string password, string ignored)
    {
        using var md5 = MD5.Create();
        var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToHexString(hash).ToLower();
    }

    public override bool VerifyHashedPassword(string password, string hash, string ignored)
    {
        return HashPassword(password, ignored).SequenceEqual(hash);
    }
}