using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DownloadMe.CommandLineParser
{
    interface ICommandLineParser
    {
        IDictionary<string, string> Parse(string[] commandLine);
    }
}
