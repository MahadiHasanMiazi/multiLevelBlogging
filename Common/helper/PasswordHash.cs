using System.Security.Cryptography;

namespace Multi_Level_Blogging_System.Common.helper;

public class PasswordHash
{
    private const int SaltSize = 16; // 16 bytes salt
    private const int HashSize = 32; // 32 bytes hash
    private const int Iterations = 100000; // 100,000 iterations
    
    public static string HashPassword(string password)
    {
        using (var rng = RandomNumberGenerator.Create())
        {
            byte[] salt = new byte[SaltSize];
            rng.GetBytes(salt);

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(HashSize);
                byte[] hashBytes = new byte[SaltSize + HashSize];
                
                Array.Copy(salt, 0, hashBytes, 0, SaltSize);
                Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

                return Convert.ToBase64String(hashBytes);
            }
        }
    }
    
    public static bool VerifyPassword(string password, string storedHash)
    {
        byte[] hashBytes = Convert.FromBase64String(storedHash);
        byte[] salt = new byte[SaltSize];
        Array.Copy(hashBytes, 0, salt, 0, SaltSize);

        using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
        {
            byte[] hash = pbkdf2.GetBytes(HashSize);
            
            for (int i = 0; i < HashSize; i++)
            {
                if (hashBytes[i + SaltSize] != hash[i])
                {
                    return false;
                }
            }
        }
        return true;
    }
}