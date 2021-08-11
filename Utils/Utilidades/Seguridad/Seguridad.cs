using System;
using System.Text;
using System.Security.Cryptography;

namespace Utilidades.Seguridad
{
    public class Seguridad
    {
        private byte[] lbtVector = { 240, 3, 45, 29, 0, 76, 173, 59 };
        private string lscryptoKey = "SistemasPlataformaMacal";

        public string Decrypt(string sQueryString)
        {
            byte[] buffer;
            TripleDESCryptoServiceProvider loCryptoClass = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider loCryptoProvider = new MD5CryptoServiceProvider();
            try
            {
                buffer = Convert.FromBase64String(sQueryString);
                loCryptoClass.Key = loCryptoProvider.ComputeHash(ASCIIEncoding.ASCII.GetBytes(lscryptoKey));
                loCryptoClass.IV = lbtVector;
                return Encoding.ASCII.GetString(loCryptoClass.CreateDecryptor().TransformFinalBlock(buffer, 0, buffer.Length));
            }
            catch (Exception ex)
            {
                throw ex;
                string mm = ex.Message;
            }
            finally
            {
                loCryptoClass.Clear();
                loCryptoProvider.Clear();
                loCryptoClass = null;
                loCryptoProvider = null;
            }

        }

        public string Encrypt(string sInputVal)
        {
            TripleDESCryptoServiceProvider loCryptoClass;
            MD5CryptoServiceProvider loCryptoProvider;
            byte[] lbtBuffer;
            try
            {
                string SEncrypt = string.Empty;

                using (loCryptoClass = new TripleDESCryptoServiceProvider())
                {
                    using (loCryptoProvider = new MD5CryptoServiceProvider())
                    {
                        lbtBuffer = System.Text.Encoding.ASCII.GetBytes(sInputVal);
                        loCryptoClass.Key = loCryptoProvider.ComputeHash(ASCIIEncoding.ASCII.GetBytes(lscryptoKey));
                        loCryptoClass.IV = lbtVector;
                        sInputVal = Convert.ToBase64String(loCryptoClass.CreateEncryptor().TransformFinalBlock(lbtBuffer, 0, lbtBuffer.Length));
                        SEncrypt = sInputVal;
                    }
                }

                //Encrypt = sInputVal;
                return SEncrypt;
            }
            catch (CryptographicException ex)
            {
                throw ex;
            }
            catch (FormatException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public string Key
        {
            get
            {
                return this.lscryptoKey;
            }
            set
            {
                this.lscryptoKey = value;
            }
        }

        public string GenerarClaveAutomatica()
        {
            DateTime ahora = DateTime.Now;
            int rInt = 90810000;
            rInt = rInt + ahora.Hour * 1000 + ahora.Second;
            try
            {
                Random r = new Random(ahora.Millisecond + ahora.Hour);
                rInt = r.Next(10000000, 99999999);
            }
            catch (Exception ex)
            {

            }
            return rInt.ToString();
        }
    }
}
