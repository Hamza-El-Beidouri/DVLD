using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD.Global_Classes
{
    public static class clsGlobal
    {

        public static clsUser CurrentUser = new clsUser();
        private const string CredentialsFileName = "Credentials.txt";
        private static byte _EncryptionKey = 2;

        private static string _EncryptText(string text)
        {

            char[] chars = text.ToCharArray();

            for (int i = 0; i < text.Length; i++)
            {
                chars[i] = (char)((int)chars[i] + _EncryptionKey);
            }

            return new string(chars);

        }

        private static string _DecryptText(string encryptedText)
        {
            char[] chars = encryptedText.ToCharArray();

            for (int i = 0; i < chars.Length; i++)
            {
                chars[i] = (char)(chars[i] - _EncryptionKey);
            }

            return new string(chars);
        }

        public static bool RememberUserNameAndPassword(string userName, string password)
        {
            try
            {
                string filePath = Path.Combine(
                                       Directory.GetCurrentDirectory(), CredentialsFileName
                                               );

                if (string.IsNullOrWhiteSpace(userName))
                {
                    if (File.Exists(filePath))
                        File.Delete(filePath);

                    return true;
                }

                string content = $"{_EncryptText(userName)}#//#{_EncryptText(password)}";
                File.WriteAllText(filePath, content);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool GetStoredCredentials(ref string userName, ref string password)
        {

            userName = string.Empty;
            password = string.Empty;

            string filePath = Path.Combine(
                                       Directory.GetCurrentDirectory(), CredentialsFileName
                                               );

            try
            {
                if (!File.Exists(filePath))
                    return false;

                string delimiter = "#//#";
                string data = File.ReadAllText(filePath);

                int index = data.IndexOf(delimiter);
                if (index == -1)
                    return false;

                userName = _DecryptText(data.Substring(0, index));
                password = _DecryptText(data.Substring(index + delimiter.Length));

                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}