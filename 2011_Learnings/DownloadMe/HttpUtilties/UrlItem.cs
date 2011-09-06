using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
namespace DownloadMe
{
    public class UrlItem : IEquatable<UrlItem>
    {
        //TODO: consider changing this to Uri type
        public readonly String Href;
        public readonly String Text;
        //TODO: Should URL know which depth it's present? SRP?
        public readonly Int32? DepthFromRootURL;

        public UrlItem(String href, String text = "", Int32? depthFromRootURL = null)
        {
            this.Href = href;
            this.Text = text;
            this.DepthFromRootURL = depthFromRootURL;
        }

        public override String ToString()
        {
            return Href + "\n\t" + Text;
        }

        public bool IsAbsoluteUrl
        {
            get
            {
                //VALIDATE INPUT FOR ALREADY ABSOLUTE URL
                if (Href.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                        Href.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                return false;
            }
        }

        public override int GetHashCode()
        {
            return Href.GetHashCode();
        }

        public override bool Equals(System.Object obj)
        {
            if (Object.ReferenceEquals(obj, (object)null))
            {
                return false;
            }

            if (Object.ReferenceEquals(this, obj))
            {
                return false;
            }

            if (this.GetType() != obj.GetType())
                return false;

            return this.Equals(obj as UrlItem);

        }
        public bool Equals(UrlItem urlItem)
        {
            return this.Href == urlItem.Href;
        }
    }
}

