using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;

namespace DownloadMe
{
    static class UrlFinder
    {
        public static List<UrlItem> GetUrlList(String content)
        {
            List<UrlItem> urlItemList = new List<UrlItem>();

            MatchCollection urlMatches = Regex.Matches(content, @"(<a.*?>.*?</a>)",
                RegexOptions.Singleline);

            foreach (Match url in urlMatches)
            {
                string value = url.Groups[1].Value;
                UrlItem urlItem;

                Match hrefMatch = Regex.Match(value, @"href=\""(.*?)\""",
                RegexOptions.Singleline);
                if (hrefMatch.Success)
                {
                    urlItem = new UrlItem(hrefMatch.Groups[1].Value, 
                                        Regex.Replace(value, @"\s*<.*?>\s*", "", RegexOptions.Singleline));

                    urlItemList.Add(urlItem);
                }
            }
            return urlItemList;
        }
    }

}
