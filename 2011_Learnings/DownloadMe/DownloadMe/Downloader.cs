using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;

namespace DownloadMe
{
    public class Downloader
    {
        private readonly String _filetype;
        private readonly Boolean _isRecursive;
        private readonly UserDetails _userDetails;
        private readonly String _sourceUrl;        
        private readonly String _targetDirectory;
        private List<LinkItem> listFileLinks = new List<LinkItem>();
        private readonly Boolean _isNetworkCredentialRequired;
        private readonly String _rootURL;


        public Downloader(String filetype, Boolean isRecursive, UserDetails userDetails, String sourceUrl, String targetDirectory, String rootURL)
        {
            _filetype = filetype;
            _isRecursive = isRecursive;
            _userDetails = userDetails;
            _sourceUrl = sourceUrl;
            _targetDirectory = targetDirectory;

            _isNetworkCredentialRequired = string.IsNullOrEmpty(userDetails.UserId) ? false : true;

            _rootURL = rootURL;
        }
        
        public void Download()
        { 
            //1. Get Directory Structure.
            //2. Download Files

        }
        private String GetUrlContent()
        {
            // used to build entire input
            StringBuilder sbUrlContent = new StringBuilder();

            // used on each read operation
            byte[] buf = new byte[8192];

            // prepare the web page we will be asking for
            HttpWebRequest request = (HttpWebRequest)
                WebRequest.Create(_sourceUrl);

            if(_isNetworkCredentialRequired)
            {
                request.Credentials = new NetworkCredential(_userDetails.UserId, _userDetails.Password, _userDetails.Domain);
            }
            // execute the request
            HttpWebResponse response = (HttpWebResponse)
                request.GetResponse();

            // we will read data via the response stream
            Stream resStream = response.GetResponseStream();

            string tempString = null;
            int count = 0;

            do
            {
                // fill the buffer with data
                count = resStream.Read(buf, 0, buf.Length);

                // make sure we read some data
                if (count != 0)
                {
                    // translate from bytes to ASCII text
                    tempString = Encoding.ASCII.GetString(buf, 0, count);

                    // continue building the string
                    sbUrlContent.Append(tempString);
                }
            }
            while (count > 0); // any more data to read?

            return sbUrlContent.ToString();
        }
        public void DownloadFiles()
        {
            WebClient webClient = new WebClient();

            if (_isNetworkCredentialRequired)
            {
                webClient.Credentials = new NetworkCredential(_userDetails.UserId, _userDetails.Password, _userDetails.Domain);
            }

            LinkFinder.Find(GetUrlContent(), _filetype, ref listFileLinks, _rootURL);

            
            int fileLimitPerThread = listFileLinks.Count / 5 ;
            
            if (listFileLinks.Count % 5 > 0)
                fileLimitPerThread++;

                      
            for (int i = 0; i < 5; i++)
            {
                if (listFileLinks.Count >= i)
                {
                    Console.WriteLine("start: {0} end: {1}",(i * fileLimitPerThread).ToString(), (i==4?listFileLinks.Count:(i + 1) * fileLimitPerThread).ToString());
                    int k = i; 
                    new Thread(() => 
                                    {
                                        DownloadFile(k * fileLimitPerThread, k == 4 ? listFileLinks.Count : (k + 1) * fileLimitPerThread);
                                    }
                               ).Start();
                }
            }
        }

        private void DownloadFile(int startIndex, int endIndex)
        {
            WebClient webClient = new WebClient();

            if (_isNetworkCredentialRequired)
            {
                webClient.Credentials = new NetworkCredential(_userDetails.UserId, _userDetails.Password, _userDetails.Domain);
            }

            string file_name = string.Empty;
            string filepath =  string.Empty;
            if (!Directory.Exists(_targetDirectory))
            {
                Directory.CreateDirectory(_targetDirectory);
            }

            for (int i = startIndex; i < endIndex ; i++)
            {
                file_name = listFileLinks[i].Href.Substring(listFileLinks[i].Href.LastIndexOf('/') + 1);
                filepath = _targetDirectory + @"\" + file_name;

                if (File.Exists(filepath))
                {
                    file_name = DateTime.Now.ToLongDateString() + "_" + file_name;
                    filepath = _targetDirectory + @"\" + file_name;
                }

                try
                {
                    //ThreadPool.QueueUserWorkItem(new WaitCallback(string.Concat(string.Concat(item.Href, "|"), filepath)));
                    webClient.DownloadFile(new Uri(listFileLinks[i].Href), filepath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(listFileLinks[i].Href.Substring(listFileLinks[i].Href.LastIndexOf('/') + 1));
                    //throw ex;
                }
            }
           
        }
    }
}
