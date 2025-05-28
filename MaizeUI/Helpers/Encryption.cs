using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

//Yeah my lover ChatGPT generated this. 
public static class Encryption
{
    // Encrypts a plain text string using a password
    public static string EncryptString(string plainText, string password)
    {
        // Generate a random salt
        byte[] salt = new byte[16];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(salt);

        // Derive a key and IV from the password and salt
        using var key = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256);
        using var aes = Aes.Create();
        aes.Key = key.GetBytes(32); // 256-bit key
        aes.IV = key.GetBytes(16);  // 128-bit IV

        using var ms = new MemoryStream();
        ms.Write(salt, 0, salt.Length); // Prepend salt

        using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
        using (var sw = new StreamWriter(cs, Encoding.UTF8))
        {
            sw.Write(plainText);
        }

        return Convert.ToBase64String(ms.ToArray());
    }

    // Decrypts a cipher text string using a password
    public static string DecryptString(string cipherText, string password)
    {
        byte[] cipherBytes = Convert.FromBase64String(cipherText);

        // Extract the salt (first 16 bytes)
        byte[] salt = new byte[16];
        Array.Copy(cipherBytes, 0, salt, 0, salt.Length);

        // Derive the key and IV from the password and salt
        using var key = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256);
        using var aes = Aes.Create();
        aes.Key = key.GetBytes(32);
        aes.IV = key.GetBytes(16);

        using var ms = new MemoryStream(cipherBytes, 16, cipherBytes.Length - 16);
        using var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read);
        using var sr = new StreamReader(cs, Encoding.UTF8);
        return sr.ReadToEnd();
    }
}
