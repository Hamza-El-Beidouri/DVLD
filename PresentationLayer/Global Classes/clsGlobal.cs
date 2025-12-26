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

        public static bool RememberUserNameAndPassword(string userName, string password)
        {
            try
            {
                string filePath = Path.Combine(
                                       Directory.GetCurrentDirectory(), "Credentials.txt"
                                               );

                if (string.IsNullOrWhiteSpace(userName))
                {
                    if (File.Exists(filePath))
                        File.Delete(filePath);

                    return true;
                }

                string content = $"{userName}#//#{password}";
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
                                       Directory.GetCurrentDirectory(), "Credentials.txt"
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

                userName = data.Substring(0, index);
                password = data.Substring(index + delimiter.Length);

                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}