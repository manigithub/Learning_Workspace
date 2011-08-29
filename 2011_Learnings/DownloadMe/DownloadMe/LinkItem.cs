using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace DownloadMe
{
    /// <summary>
    /// The below code is used from
    /// http://www.dotnetperls.com/scraping-html
    /// </summary>
    public struct LinkItem
    {
        public string Href;
        public string Text;

        public override string ToString()
        {
            return Href + "\n\t" + Text;
        }
    }

}

