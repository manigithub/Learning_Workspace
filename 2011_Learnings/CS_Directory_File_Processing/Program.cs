using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CS_Directory_File_Processing
{
    class Program
    {
        static void Main(string[] args)
        {
            DirectoryInfo diSource = new DirectoryInfo(@"C:\Banking_Fundamentals");
            PrintFilesDirectories(diSource);
            CopyDirectory(diSource, new DirectoryInfo(@"C:\Banking_Fundamentals_Copy"));
        }
        //Prints Files inside directories recursively.
        public static void PrintFilesDirectories(DirectoryInfo dir)
        {
            foreach (FileInfo file in dir.GetFiles())
            {
                Console.WriteLine("Files in the directory: {0}  file Name: {1}", dir.Name, file.Name);
            }

            foreach (DirectoryInfo directory in dir.GetDirectories())
            {
                PrintFilesDirectories(directory);
            }
        }
        /// <summary>
        /// Copy Directory - Recursively.
        /// </summary>
        /// <param name="SourceDir"></param>
        /// <param name="targetDir"></param>
        public static void CopyDirectory(DirectoryInfo SourceDir, DirectoryInfo targetDir)
		{
			if(Directory.Exists(SourceDir.FullName))
			{
                if(!Directory.Exists(targetDir.FullName))
                {
                    Directory.CreateDirectory(targetDir.FullName);
                }
			    foreach (FileInfo file in SourceDir.GetFiles())
			    {
				    //Console.WriteLine("Files in the directory: {0}  file Name: {1}", SourceDir.Name, file.Name);
                    if (!File.Exists(string.Concat(targetDir, "\\", file)))
				    File.Copy(string.Concat(SourceDir.FullName, "\\", file), string.Concat(targetDir.FullName, "\\", file));
			    }

			    foreach (DirectoryInfo directory in SourceDir.GetDirectories())
			    {
                    //CopyDirectory(string.Concat(SourceDir, "\\", directory), string.Concat(targetDir, "\\", ));
                    CopyDirectory(directory, new DirectoryInfo(string.Concat(targetDir.FullName, "\\", directory.Name)));
			    }
			}
		}
    }
}
