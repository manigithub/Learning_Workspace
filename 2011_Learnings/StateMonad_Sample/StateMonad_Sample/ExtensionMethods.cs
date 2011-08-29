using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StateMonad_Sample
{

    public static class Extensions
    {
        // Define an extention method and a default implementation
        // here so as to treat built-in types similarly to
        // user-defined types as regards the "Show" method. Anything
        // can be "Shown"

        public static void Show<a>(this a thing, int level)
        {
            Console.Write("{0}", thing.ToString());
        }
    }


}
