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
        private const int NETWORK_STREAM_READ_BUFFER_SIZE = 1024;

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

            byte[] buffer = new byte[NETWORK_STREAM_READ_BUFFER_SIZE];
            int bytesRead = 0;
            string urlContentRead = null;
            StringBuilder urlContent = new StringBuilder();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestURL);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            do
            {
                bytesRead = responseStream.Read(buffer, 0, buffer.Length);
                if (bytesRead > 0)
                {
                    urlContentRead = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    urlContent.Append(urlContentRead);
                }
            }
            while (bytesRead > 0);

            return sbUrlContent.ToString();
        }

        public String GetURLs(String Content)
        {

        }
    }

}
