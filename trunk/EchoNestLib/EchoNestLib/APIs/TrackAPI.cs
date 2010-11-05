using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EchoNestLib.APIs
{
    public class TrackAPI
    {
        
        #region Singleton
        static TrackAPI instance = null;
        static readonly object padlock = new object();

        TrackAPI()
        {
          
        }

        public static TrackAPI Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new TrackAPI();
                    }
                    return instance;
                }
            }
        }
        #endregion



        public string TrackWS = "http://developer.echonest.com/api/v4/track/";

        public void Upload(string url)
        {
            string methodParticle = "upload";

            string wsUrl = TrackWS + methodParticle;
            string postData = string.Format("api_key={0}&url={1}&wait=true&format=xml", SharedData.APIKey, url);

            SharedData.PerformPostMultipartRequest(wsUrl, postData);

        }

        

        public void Analyze(string songId, string songMd5)
        {
            if (songId == string.Empty && songMd5 == string.Empty)
                throw new ArgumentNullException("Analyze: One of the two arguments must be a non empty string");

            string methodParticle = "analyze";

            string wsUrl = TrackWS + methodParticle;
            string postData = string.Format("api_key={0}&wait=true&format=xml&bucket=audio_summary", SharedData.APIKey);

            if (songId != string.Empty)
                postData += string.Format("&id={0}", songId);

            if (songMd5 != string.Empty)
                postData += string.Format("&md5={0}", songMd5);

            SharedData.PerformPostMultipartRequest(wsUrl, postData);

            string res = SharedData.ReadPostRequestResult();

        }


    }
}
