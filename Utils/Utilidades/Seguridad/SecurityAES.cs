using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Utilidades.Seguridad
{
    public class SecurityAES
    {
        private byte[] Key;
        private byte[] IV;
        public SecurityAES(string key1, string iv1)
        {
            Key = Encoding.ASCII.GetBytes(key1);
            IV = Encoding.ASCII.GetBytes(iv1);
        }



        public string decrypt(string datosHash)
        {
            string textoFinal = "";
            try
            {
                if (!string.IsNullOrEmpty(datosHash))
                {

                    byte[] cipherText = Convert.FromBase64String(datosHash);

                    using (AesManaged aes = new AesManaged())
                    {
                        aes.KeySize = 128;
                        aes.Mode = CipherMode.CBC;
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
                                    textoFinal = reader.ReadToEnd();
                            }
                        }
                    }
                }
                else
                {
                    textoFinal = "ERROR|CADENA VACIA";
                }
            }
            catch (Exception ex)
            {
                textoFinal = "ERROR|CADENA ERRONEA DE HASH";
            }
            return textoFinal;
        }

        public string encrypt(string datosHash)
        {
            string textoFinal = "";
            try
            {
                if (!string.IsNullOrEmpty(datosHash))
                {

                    using (AesManaged aes = new AesManaged())
                    {
                        aes.KeySize = 128;
                        aes.Mode = CipherMode.CBC;
                        // Create a decryptor    
                        ICryptoTransform encryptor = aes.CreateEncryptor(Key, IV);
                        // Create the streams used for decryption.    
                        using (MemoryStream ms = new MemoryStream())
                        {
                            // Create crypto stream    
                            using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                            {
                                using (StreamWriter sw = new StreamWriter(cs))
                                    sw.Write(datosHash);
                                textoFinal = Convert.ToBase64String(ms.ToArray());
                            }
                        }
                    }
                }
                else
                {
                    textoFinal = "ERROR|CADENA VACIA";
                }
            }
            catch (Exception ex)
            {
                textoFinal = "ERROR|CADENA ERRONEA DE HASH";
            }
            return textoFinal;
        }

    }
}
