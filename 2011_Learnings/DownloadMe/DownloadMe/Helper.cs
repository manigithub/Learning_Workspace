using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DownloadMe
{
    public static class Helper
    {
        public static void ShowHelp()
        {
            Console.WriteLine("\nUsage:\n");
            Console.WriteLine("\tDownloadMe -f ppt -r -s http://www.ksvali.com/all_downloads/ -t C:\\Banking_Fundamentals -u userid -p password -d domainname\n");
            Console.WriteLine("\t -f  File Extension.\n");
            Console.WriteLine("\t -r  Recursive \n");
            Console.WriteLine("\t -s  Source Directory - URL or directory \n");
            Console.WriteLine("\t -ro Root URL  \n");
            Console.WriteLine("\t -t  Target Directory to download the files/directories \n");
            Console.WriteLine("\t -u  User Id \n");
            Console.WriteLine("\t -p  Password \n");
            Console.WriteLine("\t -d  Domain Name \n");
            //-f pdf -r -s "http://www.ksvali.com/all_downloads" -t "C:\\Banking_Fundamentals"
        }
    }
}
