using System;
using System.Collections.Specialized;
using System.IO;
using System.Security.Cryptography;

namespace Soulstone.Scanner
{
    public class Sha2Calculator
    {
        private static string PrintByteArray(byte[] array)
        {
            int i;
            string hash = string.Empty;
            for (i = 0; i < array.Length; i++)
            {
                hash += string.Format("{0:X2}", array[i]);
                if ((i % 4) == 3) Console.Write(" ");
            }

            return hash;
        }

        public static StringDictionary ComputeFolderHash(string folderPath)
        {
            var dir = new DirectoryInfo(folderPath);
            var files = dir.GetFiles();
            var mySha256 = SHA256.Create();
            var filesHash = new StringDictionary();

            foreach (FileInfo fInfo in files)
            {
                FileStream fileStream = fInfo.Open(FileMode.Open);
                fileStream.Position = 0;
                byte[] hashValue = mySha256.ComputeHash(fileStream);
                filesHash.Add(fInfo.Name, PrintByteArray(hashValue));
                fileStream.Close();
            }

            return filesHash;
        }
    }
}