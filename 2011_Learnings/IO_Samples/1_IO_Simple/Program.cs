using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace _1_IO_Simple
{
    class Program
    {
        static void Main(string[] args)
        {
             // Create a file called test.txt in the current directory:
            using (Stream s = new FileStream("test.txt", FileMode.Create ))
            {
                for (byte i = 0; i < 12; i++)
                {
                    s.WriteByte(i);
                }
            }


        }
    }
}
