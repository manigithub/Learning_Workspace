using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;

namespace DownloadMe
{
    /// <summary>
    /// The below code is used from
    /// http://www.dotnetperls.com/scraping-html
    /// </summary>
    static class LinkFinder
    {
        public static void Find(string file, ref List<LinkItem> listPDFLinks, ref List<LinkItem> listDocLinks, ref List<LinkItem> listPPTLinks)
        {
            List<LinkItem> list = null;

            // 1.
            // Find all matches in file.
            MatchCollection m1 = Regex.Matches(file, @"(<a.*?>.*?</a>)",
                RegexOptions.Singleline);

            // 2.
            // Loop over each match.
            foreach (Match m in m1)
            {
                string value = m.Groups[1].Value;
                LinkItem i = new LinkItem();

                // 3.
                // Get href attribute.
                Match m2 = Regex.Match(value, @"href=\""(.*?)\""",
                RegexOptions.Singleline);
                if (m2.Success)
                {
                    list = null;
                    if (m2.Groups[1].Value.ToLowerInvariant().IndexOf("http://") >= 0 && m2.Groups[1].Value.ToLowerInvariant().IndexOf("google") == -1)
                    {
                        if (m2.Groups[1].Value.ToLowerInvariant().Substring(m2.Groups[1].Value.Length - 4) == ".pdf")//(m2.Groups[1].Value.ToLowerInvariant().IndexOf(".pdf") >= 0)
                        {
                            list = listPDFLinks;
                        }
                        else if (m2.Groups[1].Value.ToLowerInvariant().IndexOf(".ppt") >= 0)
                        {
                            list = listDocLinks;
                        }
                        else if (m2.Groups[1].Value.ToLowerInvariant().IndexOf(".doc") >= 0)
                        {
                            list = listPPTLinks;
                        }

                        if (list != null)
                        {
                            i.Href = m2.Groups[1].Value;
                            string t = Regex.Replace(value, @"\s*<.*?>\s*", "", RegexOptions.Singleline);
                            i.Text = t;
                            list.Add(i);

                        }

                    }
                }
            }
        }

        public static void Find(String urlContent, String fileType, ref List<LinkItem> listFileLinks, String rootUrl)
        {

            // 1.
            // Find all matches in file.
            MatchCollection m1 = Regex.Matches(urlContent, @"(<a.*?>.*?</a>)",
                RegexOptions.Singleline);

            // 2.
            // Loop over each match.
            foreach (Match m in m1)
            {
                string value = m.Groups[1].Value;
                LinkItem i = new LinkItem();

                // 3.
                // Get href attribute.
                Match m2 = Regex.Match(value, @"href=\""(.*?)\""",
                RegexOptions.Singleline);
                if (m2.Success)
                {
                    //if (m2.Groups[1].Value.ToLowerInvariant().IndexOf("http://") >= 0)
                    //if (m2.Groups[1].Value.ToLowerInvariant().IndexOf("http://") >= 0 && m2.Groups[1].Value.ToLowerInvariant().IndexOf("google") == -1)
                    if (m2.Groups[1].Value.ToLowerInvariant().IndexOf(fileType) > 0)
                    {
                        //if (m2.Groups[1].Value.ToLowerInvariant().Substring(m2.Groups[1].Value.Length - 4) == ".pdf")//(m2.Groups[1].Value.ToLowerInvariant().IndexOf(".pdf") >= 0)
                        if (m2.Groups[1].Value.ToLowerInvariant().IndexOf(fileType) > 0)//(m2.Groups[1].Value.ToLowerInvariant().IndexOf(".pdf") >= 0)
                        {
                            string fileAbsoluteURL = String.Empty;
                            if (!string.IsNullOrEmpty(rootUrl))
                            {
                                fileAbsoluteURL = rootUrl + @"/" + m2.Groups[1].Value;
                            }
                            else
                            {
                                fileAbsoluteURL = m2.Groups[1].Value;
                            }
                            i.Href = fileAbsoluteURL;
                            string t = Regex.Replace(value, @"\s*<.*?>\s*", "", RegexOptions.Singleline);
                            i.Text = t;
                            listFileLinks.Add(i);
                        }

                    }

                }
            }

        }
    }

}
