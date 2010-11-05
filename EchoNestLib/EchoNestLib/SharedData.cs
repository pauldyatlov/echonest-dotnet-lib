using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EchoNestLib.Properties;
using System.Net;
using System.IO;
using SeasideResearch.LibCurlNet;

namespace EchoNestLib
{
    public static class SharedData
    {
        /// <summary>
        /// Returns the APIKey that is stored into settings
        /// </summary>
        public static string APIKey
        {
            get
            {
                return Settings.Default.APIKey;
            }
        }

        public static Int32 OnWriteData(Byte[] buf, Int32 size, Int32 nmemb,
        Object extraData)
        {

            PostRequestResult = System.Text.Encoding.UTF8.GetString(buf);
            return size * nmemb;
        }

        private static string PostRequestResult;

        /// <summary>
        /// Returns the result and empties it
        /// </summary>
        /// <returns></returns>
        public static string ReadPostRequestResult()
        {
            string res = PostRequestResult;
            PostRequestResult = string.Empty;
            return res;

        }

        public static string PerformPostMultipartRequest(string query, string postData)
        {
            Curl.GlobalInit((int)CURLinitFlag.CURL_GLOBAL_ALL);

            Easy easy = new Easy();

            Easy.WriteFunction wf = new Easy.WriteFunction(OnWriteData);
            easy.SetOpt(CURLoption.CURLOPT_WRITEFUNCTION, wf);

            
            // simple post - with a string
            easy.SetOpt(CURLoption.CURLOPT_POSTFIELDS,
                postData);

            easy.SetOpt(CURLoption.CURLOPT_USERAGENT,
                "C# EchoNest Lib");
            easy.SetOpt(CURLoption.CURLOPT_FOLLOWLOCATION, true);
            easy.SetOpt(CURLoption.CURLOPT_URL,
                query);
            easy.SetOpt(CURLoption.CURLOPT_POST, true);

           
            CURLcode res = easy.Perform();

            if (res != CURLcode.CURLE_OK)
                throw new WebException("Post operation failed");
    
           
            
            easy.Cleanup();
            string code = string.Empty;
            


            Curl.GlobalCleanup();
            
            
            return string.Empty;

            //ASCIIEncoding encoding = new ASCIIEncoding();
            //string postData = string.Format("api_key={0}&url={1}&wait=true&format=xml", SharedData.APIKey, fileUrl);

            //byte[] data = encoding.GetBytes(postData);

            //// Prepare web request...
            //HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(query);
            //myRequest.Method = "POST";
            //myRequest.UserAgent = "C# EchoNest Lib";

            ////myRequest.ContentType = "multipart/form-data";
            //myRequest.ContentLength = data.Length;
            //Stream newStream = myRequest.GetRequestStream();
            //// Send the data.
            //newStream.Write(data, 0, data.Length);
            //newStream.Close();


            //HttpWebResponse response = null;

            //string responseBody;

            //int statusCode;


            //try
            //{

            //    response = (HttpWebResponse)myRequest.GetResponse();
            //    //byte[] buf = new byte[8192];
            //    Stream respStream = response.GetResponseStream();
            //    StreamReader respReader = new StreamReader(respStream);



            //    responseBody = respReader.ReadToEnd();
            //    statusCode = (int)(HttpStatusCode)response.StatusCode;

            //}
            //catch (WebException ex)
            //{
            //    response = (HttpWebResponse)ex.Response;
            //    responseBody = "No Server Response";


            //}

            //return responseBody;
        }

        public static string PerformGetRequest(string query)
        {

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(query);

            request.Method = "GET";
            request.UserAgent = "C# EchoNest Lib";
            //request.ContentLength = 8192;
            HttpWebResponse response = null;

            string responseBody;

            int statusCode;


            try
            {

                response = (HttpWebResponse)request.GetResponse();
                //byte[] buf = new byte[8192];
                Stream respStream = response.GetResponseStream();
                StreamReader respReader = new StreamReader(respStream);



                responseBody = respReader.ReadToEnd();
                statusCode = (int)(HttpStatusCode)response.StatusCode;

            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;
                responseBody = "No Server Response";


            }

            return responseBody;

        }
    }
}
