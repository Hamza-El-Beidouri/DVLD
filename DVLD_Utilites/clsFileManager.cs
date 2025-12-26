using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Utilites
{
    public static class clsFileManager
    {

        static string peopleImagesPath = @"C:\Users\PC\Pictures\DVLD-People-Images";

        public static string SaveImageToPeopleFolder(string sourceFilePath, string oldFilePath)
        {            

            // Delete old image if it exists
            if (!string.IsNullOrEmpty(oldFilePath) && File.Exists(oldFilePath))
            {
                File.Delete(oldFilePath);
            }

            // Step 1: creates directory if it doesn't exist
            Directory.CreateDirectory(peopleImagesPath);

            // Step 2: assign new file name while keeping the same extension
            string newFileName = Guid.NewGuid().ToString() + Path.GetExtension(sourceFilePath);

            // Step 3: Build full destination path
            string destinationFilePath = Path.Combine(peopleImagesPath, newFileName);

            // Step 4: Copy the file to the destination
            File.Copy(sourceFilePath, destinationFilePath);

            // Step 5: return file name
            return destinationFilePath;

        }

    }
}
