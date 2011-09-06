using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DownloadMe
{
    public class StartUp
    {
        public static void Main(String[] args)
        {
            HttpUtilities httpUtilities = HttpUtilities.Instance;

            try
            {
                //List<List<UrlItem>> listOfUrlItemList = httpUtilities.GetUrlList(new Uri("http://www.textbooksonline.tn.nic.in/"), 1, "http://www.textbooksonline.tn.nic.in");
                Dictionary<UrlItem, int> urlList = httpUtilities.GetUrlList(new Uri("http://www.textbooksonline.tn.nic.in/"), 2, "http://www.textbooksonline.tn.nic.in", false);
                Console.WriteLine("Total Urls: {0} ", urlList.Count);
                foreach (var urlItem in urlList)
                {
                    Console.WriteLine("HyperLink: {0} ", urlItem.Key.Href);
                    Console.WriteLine("Depth: {0} ", urlItem.Key.DepthFromRootURL);
                }
              
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception {0} ", ex.Message);
            }


        }
    }
}
