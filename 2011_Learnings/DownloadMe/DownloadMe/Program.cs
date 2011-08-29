using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DownloadMe.CommandLineParser;

namespace DownloadMe
{
    class Program
    {
        static void Main(string[] args)
        {
            CommandLineParser.CommandLine cmdLine = new CommandLine(args);
            Downloader downloader = null;

            if (!cmdLine.IsValid())
            {
                Helper.ShowHelp();
                Environment.Exit(0);
            }

            try
            {
               IDictionary<string, string> cmdLineDictionary = cmdLine.Parse();
               String filetype = cmdLineDictionary.ContainsKey("-f")? String.Concat(".", cmdLineDictionary["-f"]): "." ;
               Boolean isRecursive = cmdLineDictionary.ContainsKey("-r");
               UserDetails userDetails = cmdLineDictionary.ContainsKey("-u")? new UserDetails(cmdLineDictionary["-u"], cmdLineDictionary["-p"], cmdLineDictionary["-d"]): new UserDetails();
               String sourceUrl = cmdLineDictionary["-s"];
               String targetDirectory = cmdLineDictionary["-t"];
               String rootURL = cmdLineDictionary.ContainsKey("-ro")?cmdLineDictionary["-ro"]:null;

               downloader = new Downloader(filetype, isRecursive, userDetails, sourceUrl, targetDirectory, rootURL);
               downloader.DownloadFiles();
            }
            catch (Exception ex)
            {
                Helper.ShowHelp();
                Environment.Exit(0);
            }
            Console.ReadLine();
        }

    }
}
