using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace StateMonad_Sample
{
    //Given a State, this method returns, state and content pair.
    public delegate Scp<a> S2Scp<a>(int state);
    
    //Given an input a, it returns a new object of type SM<b>
    public delegate SM<b> Maker<a, b>(a input);
    
    class Program
    {



        static void Main(string[] args)
        {
            //Simple Tree with string as contents in Leaf nodes
            Tr<string> t = GetTree();
            Console.WriteLine("Simple Tree:\n");
            t.Show(2);
            Console.WriteLine();

            //This gives the new object with state and content, 
            //without changing/modifying original object.
            Console.WriteLine("\nNew Tree with Contents and State:\n");
            var t3 = MLabel<string>(t);
            t = null;
            t3.Show(2);

            Console.ReadLine();
        }

        public static Br<string> GetTree()
        {
            var t = new Br<string>
            {
                left = new Lf<string> { contents = "a" },
                right = new Br<string>
                {
                    left = new Br<string>
                    {
                        left = new Lf<string> { contents = "b" },
                        right = new Lf<string> { contents = "c" }
                    },
                    right = new Lf<string> { contents = "d" }
                },
            };
            return t;
        }

        //Given a Tree with contents, returns a tree with state and contents.
        public static Tr<Scp<a>> MLabel<a>(Tr<a> t)
        {
            // throw away the label, we're done with it.

            return MkM(t).s2scp(0).lcpContents;
        }

        //
        private static SM<Tr<Scp<a>>> MkM<a>(Tr<a> t)
        {
            if (t is Lf<a>)
            {
                // Call UpdateState to get an instance of
                // SM<int>. Shove it (@bind it) through a lambda
                // expression that converts ints to SM<Tr<Scp<a>>
                // using the "closed-over" contents from the input
                // Leaf node:

                var lf = (t as Lf<a>);

                return SM<int>.@bind
                (UpdateState(),
                    (n => SM<Tr<Scp<a>>>.@return
                        (new Lf<Scp<a>>
                        {
                            contents = new Scp<a>
                            {
                                label = n,
                                lcpContents = lf.contents
                            }
                        }
                        )
                    )
                );
            }
            else if (t is Br<a>)
            {
                var br = (t as Br<a>);
                var oldleft = br.left;
                var oldright = br.right;

                return SM<a>.@bind(
                return SM<Tr<Scp<a>>>.@bind
                (MkM<a>(oldleft),//static Method Call - recursive - call to same method
                    (newleft => SM<Tr<Scp<a>>>.@bind
                        (MkM<a>(oldright), //static method call - recursive - call to same method
                            (newright => SM<Tr<Scp<a>>>.@return
                                (new Br<Scp<a>>
                                {
                                    left = newleft,
                                    right = newright
                                }
                                )
                            )
                        )
                    )
                );
            }
            else
            {
                throw new Exception("MakeMonad/MLabel: impossible tree subtype");
            }
        }
        
        //input: nothing
        //Output:
            //1. SM<int> - Which has a delegate field assigned with a lambda expression.
        private static SM<int> UpdateState()
        {   
            SM<int> obj = new SM<int>
            {
                s2scp = (n => new Scp<int>
                {

                    label = n + 1, 
                    lcpContents = n
                })
            };            
            return obj;
            
        }



    }
}
