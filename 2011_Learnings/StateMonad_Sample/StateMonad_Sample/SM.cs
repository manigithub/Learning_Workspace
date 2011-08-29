using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StateMonad_Sample
{
    public class SM<a>
    {
        // Here is the meat: the only data member of this monad:

        //This is delegate, which accepts an integer as label and returns Scp<a>
        public S2Scp<a> s2scp { get; set; }

        //input: a contents
            // 1st time: Lf<Scp<string>> with label: 0 lcpContents: "a"
            // 2nd time: Lf<Scp<string>> with label: 1 lcpContents: "b"
            // 3rd time: Lf<Scp<string>> with label: 2 lcpContents: "c"
            // 4th time: Lf<Scp<string>> with label: 3 lcpContents: "d"
        //output:
        public static SM<a> @return(a contents)
        {
            Console.WriteLine("\nInside @return method: {0}", DateTime.Now.Millisecond.ToString());
            Console.WriteLine("\nInput:\t{0}", contents.ToString());
            

            SM<a> newObj = new SM<a>
            {
                s2scp = (st => new Scp<a>
                {
                    label = st,
                    lcpContents = contents
                })
            };

            Console.WriteLine("\n\t @return before returning: {0}", newObj.ToString());
            return newObj;
        }

       
        //Given input SM<a> and input b, returns SM<b>
        public static SM<b> @bind<b>(SM<a> inputMonad, Maker<a, b> inputMaker)
        {
            Console.WriteLine("\nInside @bind method:{0}", DateTime.Now.Millisecond.ToString());
            Console.WriteLine("\nInput:\t{0}",inputMonad.ToString());
            Console.WriteLine("\nInput:\t{0}", inputMaker.ToString());

            SM<b> newObj = new SM<b>
            {
                // The new instance of the state monad is a
                // function from state to state-contents pair,
                // here realized as a C# lambda expression:

                s2scp = (st0 =>
                {
                    // Deconstruct the result of calling the input
                    // monad on the state parameter (done by
                    // pattern-matching in Haskell, by hand here):

                    var lcp1 = inputMonad.s2scp(st0);  //delegate is invoked and the lambda expression in updatestate() is called.
                    var state1 = lcp1.label;
                    var contents1 = lcp1.lcpContents;

                    // Call the input maker on the contents from
                    // above and apply the resulting monad
                    // instance on the state from above:

                    return inputMaker(contents1).s2scp(state1);

                })
            };
            Console.WriteLine("\n\t @bind before returning {0}", newObj.ToString());
            return newObj;
        }
    }
}
