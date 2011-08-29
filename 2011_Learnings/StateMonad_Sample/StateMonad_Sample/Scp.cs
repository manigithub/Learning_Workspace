using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StateMonad_Sample
{

    // The plan is to convert trees containing "content" data into
    // trees containing pairs of contents and labels.

    // In Haskell, just construct the type "pair of state and
    // contents" on-the-fly as a pair-tuple of type (S, a). In
    // C#, create a class such pairs since tuples are not
    // primitive as they are in Haskell.

    // The first thing we need is a class or type for
    // state-content pairs, call it Scp.  Since the type of the
    // state is hard-coded as "Int," Scp<a> has only one type
    // parameter, the type a of its contents.

    public class Scp<a> // State-Content Pair
    {
        public int label { get; set; }
        public a lcpContents { get; set; } // New name; don't confuse
        // with the old "contents"
        public override string ToString()
        {
            return String.Format("Label: {0}, Contents: {1}", label, lcpContents);
        }
    }
}
