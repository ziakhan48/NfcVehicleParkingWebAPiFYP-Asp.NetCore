using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace WorkerService.Core
{
    public static class Utility
    {
        public static  void PostData(Object T)
        {
            var request = (HttpWebRequest)WebRequest.Create("/api/");

            var postData = "thing1=" + Uri.EscapeDataString("hello");
            postData += "&thing2=" + Uri.EscapeDataString("world");
            string s = JsonConvert.SerializeObject(T);
            var data = Encoding.ASCII.GetBytes(s);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
        }
    }
}
