using DVLD_BusinessLayer;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace DVLD.Global_Classes
{
    public static class clsGlobal
    {
        public static clsUser CurrentUser = new clsUser();
        private const string CredentialsFileName = "Credentials.json";
        private static byte _EncryptionKey = 2;

        private static string _EncryptText(string text)
        {
            char[] chars = text.ToCharArray();

            for (int i = 0; i < chars.Length; i++)
                chars[i] = (char)(chars[i] + _EncryptionKey);

            return new string(chars);
        }

        private static string _DecryptText(string text)
        {
            char[] chars = text.ToCharArray();

            for (int i = 0; i < chars.Length; i++)
                chars[i] = (char)(chars[i] - _EncryptionKey);

            return new string(chars);
        }

        [DataContract]
        private class clsCredentials
        {
            [DataMember]
            public string UserName { get; set; }

            [DataMember]
            public string Password { get; set; }
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

                clsCredentials credentials = new clsCredentials
                {
                    UserName = _EncryptText(userName),
                    Password = _EncryptText(password)
                };

                DataContractJsonSerializer serializer =
                    new DataContractJsonSerializer(typeof(clsCredentials));

                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.WriteObject(stream, credentials);
                    string jsonString = Encoding.UTF8.GetString(stream.ToArray());
                    File.WriteAllText(filePath, jsonString);
                }

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

                DataContractJsonSerializer serializer =
                    new DataContractJsonSerializer(typeof(clsCredentials));

                using (FileStream stream = new FileStream(filePath, FileMode.Open))
                {
                    clsCredentials credentials = (clsCredentials)serializer.ReadObject(stream);
                    if (credentials == null)
                        return false;

                    userName = _DecryptText(credentials.UserName);
                    password = _DecryptText(credentials.Password);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
