using DVLD_BusinessLayer;
using Microsoft.Win32;
using System;
using System.IO;

namespace DVLD.Global_Classes
{
    public static class clsGlobal
    {
        public static clsUser CurrentUser = new clsUser();
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

        public static bool RememberUserNameAndPassword(string username, string password)
        {

            const string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLD";

            try
            {

                Registry.SetValue(keyPath, "Username", username, RegistryValueKind.String);

                // TODO: Encrypt this password before saving!
                Registry.SetValue(keyPath, "Password", _EncryptText(password), RegistryValueKind.String);

                return true;
            }
            catch (Exception ex)
            {
                // In a real app, you might want to log the error here
                return false;
            }
        }

        public static bool GetStoredCredentials(ref string userName, ref string password)
        {
            // Initialize parameters to empty to ensure a clean state
            userName = string.Empty;
            password = string.Empty;

            string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLD";

            try
            {
                // Registry.GetValue returns null if the key or value doesn't exist
                object storedUser = Registry.GetValue(keyPath, "Username", null);
                object storedPass = Registry.GetValue(keyPath, "Password", null);

                if (storedUser == null || storedPass == null)
                    return false;

                userName = storedUser.ToString();
                password = _DecryptText(storedPass.ToString());

                return true;
            }
            catch (Exception ex)
            {
                // If the registry is inaccessible or another error occurs
                return false;
            }
        }
    }

}
