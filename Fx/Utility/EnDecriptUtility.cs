namespace XuanLibrary.Utility
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    public static class EnDecriptUtility
    {
        private const string PasswordSalt = "AMBIENT_CONFIGURATION_PASSWORD_SALT";

        #region AES Encript/Decript
        public static string Encrypt(string strInput, string strPassword)
        {
            return Convert.ToBase64String(Encrypt(Encoding.UTF8.GetBytes(strInput), strPassword));
        }

        public static string Decrypt(string strInput, string strPassword)
        {
            return Encoding.UTF8.GetString(Decrypt(Convert.FromBase64String(strInput), strPassword));
        }

        private static byte[] Encrypt(byte[] arrbyInput, string strPassword)
        {
            using (var objAesManaged = new AesManaged())
            {
                var objDeriveBytes = new Rfc2898DeriveBytes(strPassword, Encoding.UTF8.GetBytes(PasswordSalt));
                objAesManaged.Key = objDeriveBytes.GetBytes(objAesManaged.KeySize / 8);
                objAesManaged.IV = objDeriveBytes.GetBytes(objAesManaged.BlockSize / 8);
                using (var objMemoryStream = new MemoryStream())
                {
                    using (var objTransform = objAesManaged.CreateEncryptor())
                    {
                        using (var objCryptoStream = new CryptoStream(objMemoryStream, objTransform, CryptoStreamMode.Write))
                        {
                            objCryptoStream.Write(arrbyInput, 0, arrbyInput.Length);
                            objCryptoStream.Close();
                        }
                    }
                    return objMemoryStream.ToArray();
                }
            }
        }

        private static byte[] Decrypt(byte[] arrbyInput, string strPassword)
        {
            using (var objAesManaged = new AesManaged())
            {
                var objDeriveBytes = new Rfc2898DeriveBytes(strPassword, Encoding.UTF8.GetBytes(PasswordSalt));
                objAesManaged.Key = objDeriveBytes.GetBytes(objAesManaged.KeySize / 8);
                objAesManaged.IV = objDeriveBytes.GetBytes(objAesManaged.BlockSize / 8);
                using (var objMemoryStream = new MemoryStream())
                {
                    using (var objTransform = objAesManaged.CreateDecryptor())
                    {
                        using (var objCryptoStream = new CryptoStream(objMemoryStream, objTransform, CryptoStreamMode.Write))
                        {
                            objCryptoStream.Write(arrbyInput, 0, arrbyInput.Length);
                            objCryptoStream.Close();
                        }
                    }
                    return objMemoryStream.ToArray();
                }
            }
        }
        #endregion
    }
}
