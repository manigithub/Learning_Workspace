using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StateMonad_Sample
{
    
    public abstract class Tr<T>
    {
        public abstract void Show(int level);
    }
    
    // A Tr<a> is either a Lf<a> or a Br<a>.
    public class Lf<T> : Tr<T>
    {
        public T contents { get; set; }

        public override void Show(int level)
        {
            Console.Write(new String(' ', level * Constants.indentation));
            Console.Write("Leaf: ");
            contents.Show(level);
            Console.WriteLine();
        }
    }

    public class Br<a> : Tr<a>
    {
        public Tr<a> left { get; set; }
        public Tr<a> right { get; set; }

        public override void Show(int level)
        {
            Console.Write(new String(' ', level * Constants.indentation));
            Console.WriteLine("Branch:");
            left.Show(level + 1);
            right.Show(level + 1);
        }
    }
}
