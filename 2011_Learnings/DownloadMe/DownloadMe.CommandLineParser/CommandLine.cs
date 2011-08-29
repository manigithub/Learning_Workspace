using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DownloadMe.CommandLineParser
{
    public class CommandLine
    {
        private readonly string[] commandLine;
        
        public CommandLine(string[] commandLine)
        {
            if (commandLine == null)
            {
                throw new Exception("Show Help");
            }
            this.commandLine = commandLine;
        }
        
        public IDictionary<string, string> Parse()
        {
            IDictionary<string, string> commandHashtable = new Dictionary<string, string>();
            for (int i = 0; i < commandLine.Length; i++)
            {
                if (commandLine[i][0] == '-' && commandLine[i+1][0] == '-')
                {
                    if(commandHashtable.ContainsKey(commandLine[i]))
                    {
                       throw new Exception("Invalid command - Same switch options twice");
                    }
                    commandHashtable.Add(commandLine[i], null);
                }
                else
                {
                    if(commandHashtable.ContainsKey(commandLine[i]))
                    {
                        throw new Exception("Invalid command - Same switch options twice");
                        
                    }
                    commandHashtable.Add(commandLine[i], commandLine[i + 1]);
                    i++;
                }
            }
            return commandHashtable;
        }
        
        public int ParameterCount()
        {
            return commandLine.Length;
        }
         
        public bool IsValid()
        {
            bool isValid = false;

            if (ParameterCount() < 3)
            {
                return isValid;
            }
            for (int i = 0; i < commandLine.Length; i++)
            {
                if (i==0 && commandLine[i][0] != '-')
                {
                    return isValid;
                }

                else if (commandLine[i][0] != '-' && commandLine[i-1][0] != '-')
                {
                    return isValid;
                }
            }
            //Check URL/Network drive/local drive/
            //Check share point drive or not ---logic should be different.

            isValid = true;

            return isValid;
        }
    }
}
