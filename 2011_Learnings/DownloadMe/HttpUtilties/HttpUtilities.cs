using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mime;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.IO;

namespace DownloadMe
{
    public sealed class HttpUtilities
    {
        private static readonly HttpUtilities instance = new HttpUtilities();
        private const int STREAM_READ_BUFFER_SIZE = 1024;

        static HttpUtilities()
        {

        }
        private HttpUtilities()
        {

        }
        public static HttpUtilities Instance
        {
            get
            {
                return instance;
            }
        }

        public String GetUrlContent(String requestURL)
        {
            if (requestURL == null)
                throw new ArgumentNullException("requestURL");

            return GetUrlContent(new Uri(requestURL));
        }

        public String GetUrlContent(Uri requestURL)
        {
            if (requestURL == (Uri)null)
                throw new ArgumentNullException("requestURL");

            //Console.WriteLine(requestURL.ToString());

            StringBuilder urlContent = new StringBuilder();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestURL);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();

            return GetContentFromStream(responseStream);
        }

        public string GetContentFromStream(Stream stream)
        {
            if (stream == (Stream)null)
                throw new ArgumentNullException("stream");

            byte[] buffer = new byte[STREAM_READ_BUFFER_SIZE];
            int bytesRead = 0;
            StringBuilder content = new StringBuilder();
            string contentRead = null;

            do
            {
                bytesRead = stream.Read(buffer, 0, buffer.Length);
                if (bytesRead > 0)
                {
                    //contentRead = Encoding.Unicode.GetString(buffer, 0, bytesRead);
                    contentRead = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    content.Append(contentRead);
                }
            } while (bytesRead > 0);

            return content.ToString();
        }

        public List<UrlItem> GetUrlList(String content)
        {
            if (content == (String)null)
                throw new ArgumentNullException("content");

            return UrlFinder.GetUrlList(content);
        }

        //TODO: I Struggled with recursion here.  Need to learn recursion clearly

        /* public List<List<UrlItem>> GetUrlList(Uri url, int urlDepth, String rootUrl = "")
        {
            if (url == (Uri)null)
                throw new ArgumentNullException("url");

            List<List<UrlItem>> listOfUrlItemList = new List<List<UrlItem>>();


            List<UrlItem> urlList = new List<UrlItem>();

            if (!url.IsAbsoluteUri)
            {
                listOfUrlItemList.Add(urlList);
                return listOfUrlItemList;
            }

            urlList.Add(new UrlItem((url.AbsoluteUri)));

            if (urlDepth == 0)
            {
                listOfUrlItemList.Add(urlList);
                return listOfUrlItemList;
            }
            else
            {
                string urlContent = GetUrlContent(url);

                urlList = GetUrlList(urlContent);
                List<UrlItem> absoluteUrlItems = new List<UrlItem>();

                foreach (var urlItem in urlList)
                {
                    if ((!urlItem.IsAbsoluteUrl))
                    {
                        Uri root = new Uri(rootUrl);
                        if (root.IsAbsoluteUri)
                        {
                            UrlItem UrlItemAbsolute = new UrlItem(String.Concat(String.Concat(rootUrl, @"/"), urlItem.Href), urlItem.Text);
                            if (!(IsUrlExists(UrlItemAbsolute, absoluteUrlItems)))
                            {
                                absoluteUrlItems.Add(UrlItemAbsolute);
                                //List<List<UrlItem>> a = GetUrlList(url: new Uri(UrlItemAbsolute.Href), urlDepth: --urlDepth, rootUrl: "http://www.textbooksonline.tn.nic.in");
                                //foreach (var urlItemList in a)
                                //{
                                //    listOfUrlItemList.Add(urlItemList);
                                //}
                            }
                        }
                    }
                }
                listOfUrlItemList.Add(absoluteUrlItems);
                foreach (var item in absoluteUrlItems)
                {
                    List<List<UrlItem>> a = GetUrlList(url: new Uri(item.Href), urlDepth: --urlDepth, rootUrl: "http://www.textbooksonline.tn.nic.in");
                }
                return listOfUrlItemList;
            }
        }

        private Boolean IsUrlExists(UrlItem urlItem, List<UrlItem> UrlItems)
        {
            foreach (var url in UrlItems)
            {
                if (urlItem.Equals(url))
                {
                    return true;
                }
            }


            return false;
        } */

        public Dictionary<UrlItem, int> GetUrlList(Uri parentUrl, int urlSearchDepth, String rootUrl = "", Boolean uniqueURLs = true)
        {
            if (parentUrl == (Uri)null)
                throw new ArgumentNullException("parentUrl");

            Dictionary<UrlItem, int> urlList = new Dictionary<UrlItem, int>();
            //List<UrlItem> urlList = new List<UrlItem>();

            //if (!parentUrl.IsAbsoluteUri)
            //{
            //    listOfUrlItemList.Add(urlList);
            //    return listOfUrlItemList;
            //}

            urlList.Add(new UrlItem(href: parentUrl.AbsoluteUri, depthFromRootURL: 0), 0);

            if (urlSearchDepth == 0)
            {
                return urlList;
            }

            String urlContent = String.Empty;
            List<UrlItem> currentUrlList = null;
            Uri currentUrl = parentUrl;
            List<UrlItem> previousDepthUrlList = new List<UrlItem>();

            for (int depth = 1; depth <= urlSearchDepth; depth++)
            {
                previousDepthUrlList.Clear();

                var previousDepthUrlItems = (from item in urlList
                                             where item.Value == depth - 1
                                             select item.Key).ToList<UrlItem>();

                //previousDepthUrlList.AddRange(urlList.Where((url) => url.DepthFromRootURL == depth - 1));
                previousDepthUrlList.AddRange(previousDepthUrlItems);

                foreach (var urlitem in previousDepthUrlList)
                {
                    try
                    {
                        urlContent = GetUrlContent(urlitem.Href);
                    }
                    catch (Exception ex)
                    {
                        urlContent = null;
                    }

                    if (null != urlContent)
                    {
                        //List of URLs present in Current Url
                        currentUrlList = GetUrlList(urlContent);

                        foreach (var url in currentUrlList)
                        {
                            String href = url.Href;
                            String text = url.Text;

                            if (!url.IsAbsoluteUrl)
                            {
                                href = String.Concat(String.Concat(rootUrl, @"/"), url.Href);
                            }
                            if (Uri.IsWellFormedUriString(href, UriKind.Absolute))
                            {
                                UrlItem urlTobeAdded = new UrlItem(href, text, depth);
                                bool isUrlNew = true;
                                foreach (var item in urlList)
                                {
                                    UrlItem urlExisting = item.Key;
                                    if (urlExisting.Equals(urlTobeAdded))
                                    {
                                        isUrlNew = false;
                                        break;
                                    }

                                }
                                if (isUrlNew)
                                {
                                    urlList.Add(new UrlItem(href, text, depth), depth);
                                }
                            }
                        }
                    }
                }
            }
            return urlList;
        }
    }
}
