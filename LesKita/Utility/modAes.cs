using System.Security.Cryptography;
using System.Text;

namespace LesKita;

public static class modAes
{
    public static void EncryptAesManaged(string raw)
    {
        try
        {
            // Create Aes that generates a new key and initialization vector (IV).    
            // Same key must be used in encryption and decryption    
            using (AesManaged aes = new AesManaged())
            {
                // Encrypt string    
                byte[] encrypted = Encrypt(raw);
                // Print encrypted string    
                Console.WriteLine($"Encrypted data: {System.Text.Encoding.UTF8.GetString(encrypted)}");
                // Decrypt the bytes to a string.    
                string decrypted = Decrypt(encrypted);
                // Print decrypted string. It should be same as raw data    
                Console.WriteLine($"Decrypted data: {decrypted}");
            }
        }
        catch (Exception exp)
        {
            Console.WriteLine(exp.Message);
        }
    }
    public static byte[] Encrypt(string plainText)
    {
        byte[] Key = Encoding.UTF8.GetBytes("1234567890abcdef"); // 16-byte key
        byte[] IV = Encoding.UTF8.GetBytes("abcdef1234567890");  // 16-byte IV

        byte[] encrypted;
        // Create a new AesManaged.    
        using (Aes aes = Aes.Create())
        {
            // Create encryptor    
            ICryptoTransform encryptor = aes.CreateEncryptor(Key, IV);
            // Create MemoryStream    
            using (MemoryStream ms = new MemoryStream())
            {
                // Create crypto stream using the CryptoStream class. This class is the key to encryption    
                // and encrypts and decrypts data from any given stream. In this case, we will pass a memory stream    
                // to encrypt    
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    // Create StreamWriter and write data to a stream    
                    using (StreamWriter sw = new StreamWriter(cs))
                        sw.Write(plainText);
                    encrypted = ms.ToArray();
                }
            }
        }
        // Return encrypted data    
        return encrypted;
    }
    public static string Decrypt(byte[] cipherText)
    {
        byte[] Key = Encoding.UTF8.GetBytes("1234567890abcdef"); // 16-byte key
        byte[] IV = Encoding.UTF8.GetBytes("abcdef1234567890");  // 16-byte IV
        string plaintext = null;
        // Create AesManaged    
        using (AesManaged aes = new AesManaged())
        {
            // Create a decryptor    
            ICryptoTransform decryptor = aes.CreateDecryptor(Key, IV);
            // Create the streams used for decryption.    
            using (MemoryStream ms = new MemoryStream(cipherText))
            {
                // Create crypto stream    
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                {
                    // Read crypto stream    
                    using (StreamReader reader = new StreamReader(cs))
                        plaintext = reader.ReadToEnd();
                }
            }
        }
        return plaintext;
    }
}
