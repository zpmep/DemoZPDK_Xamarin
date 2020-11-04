using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using PCLCrypto;

namespace DemoZPDK_Xaramin_V2.Helper.Crypto
{
    public class RSAHelper
    {
        public static string Encrypt(string data, string publicKey)
        {
            //byte[] publicKeyBytes = Convert.FromBase64String(publicKey);

            byte[] keyBytes = Encoding.UTF8.GetBytes(publicKey);
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            var algorithm = WinRTCrypto.MacAlgorithmProvider.OpenAlgorithm(MacAlgorithm.HmacSha1);
            CryptographicHash hasher = algorithm.CreateHash(keyBytes);
            hasher.Append(dataBytes);
            byte[] mac = hasher.GetValueAndReset();

            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < mac.Length; i++)
            {
                sBuilder.Append(mac[i].ToString("X2"));
            }
            return sBuilder.ToString().ToLower();

            //You can then easily import the key parameters into RSACryptoServiceProvider:
            //RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            //rsa.ImportParameters(rsaParameters);
            
            ////Finally, do your encryption:
            //byte[] dataToEncrypt = Encoding.UTF8.GetBytes(data);
            //// Sign data with Pkcs1
            //byte[] encryptedData = rsa.Encrypt(dataToEncrypt, false);
            //// Convert Bytes to Hash
            //var hash = Convert.ToBase64String(encryptedData);

            //return hash;
        }
        public static string EncryptV1(string data, string publicKey)
        {
            string hash = "";
            try
            {
                byte[] keys = Convert.FromBase64String(publicKey);
                X509Certificate2 cert = new X509Certificate2(keys);
                hash = Encrypt(data, cert);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return hash;
        }
        public static string Encrypt(string plainText, X509Certificate2 cert)
        {
            RSACryptoServiceProvider publicKey = (RSACryptoServiceProvider)cert.PublicKey.Key;
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            
            byte[] encryptedBytes = publicKey.Encrypt(plainBytes, false);
            string encryptedText = Convert.ToBase64String(encryptedBytes);
            return encryptedText;
        }

        public static string Decrypt(string encryptedText, X509Certificate2 cert)
        {
            RSACryptoServiceProvider privateKey = (RSACryptoServiceProvider)cert.PrivateKey;
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
            byte[] decryptedBytes = privateKey.Decrypt(encryptedBytes, false);
            string decryptedText = Encoding.UTF8.GetString(decryptedBytes);
            return decryptedText;
        }
    }
}