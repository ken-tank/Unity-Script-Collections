using System.Security.Cryptography;
using System.Text;
using System;

public class StringCryption
{
    static string key = "AB452FE321FFAAEEAC425531236778F2";
    static string iv = "3241563845125263";

    public static string Encrypt(string text) 
    { 
        Aes aes = Aes.Create();
        aes.BlockSize = 128;
        aes.KeySize = 256;
        aes.Key = ASCIIEncoding.ASCII.GetBytes(Protection.key);
        aes.IV = ASCIIEncoding.ASCII.GetBytes(Protection.iv);
        aes.Padding = PaddingMode.PKCS7;

        byte[] txByteData = ASCIIEncoding.ASCII.GetBytes(text);
        ICryptoTransform trnfm = aes.CreateEncryptor(aes.Key, aes.IV);

        byte[] result = trnfm.TransformFinalBlock(txByteData, 0, txByteData.Length);
        return Convert.ToBase64String(result);
    }

    public static string Decrypt(string text) 
    {
        Aes aes = Aes.Create();
        aes.BlockSize = 128;
        aes.KeySize = 256;
        aes.Key = ASCIIEncoding.ASCII.GetBytes(Protection.key);
        aes.IV = ASCIIEncoding.ASCII.GetBytes(Protection.iv);
        aes.Padding = PaddingMode.PKCS7;

        byte[] txByteData = Convert.FromBase64String(text);
        ICryptoTransform trnfm = aes.CreateDecryptor();

        byte[] result = trnfm.TransformFinalBlock(txByteData, 0, txByteData.Length);
        return ASCIIEncoding.ASCII.GetString(result);
    }
}
