using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ElectricSheep.EchoNestLib.BObject.Track;
using System.Xml;
using System.Xml.XPath;
using System.IO;

namespace ElectricSheep.EchoNestLib.APIs
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

        #region Analyze
        public Track Analyze(string songId)
        {
            return this.Analyze(songId, string.Empty);
        }


        public Track Analyze(string songId, string songMd5)
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

            string res = SharedData.ReadWebRequestResult();

            return this.AnalyzeParseXml(res);
        }


        public Track AnalyzeParseXml(string xml)
        {
            Track t = new Track();

            XmlReader reader = XmlReader.Create(new StringReader(xml));

            XPathDocument doc = new XPathDocument(reader);
            XPathNavigator nav = doc.CreateNavigator();

            XPathExpression expr;
            expr = nav.Compile("/response/track");
            XPathNodeIterator iterator = nav.Select(expr);

            while (iterator.MoveNext())
            {
             
                XPathNodeIterator trackIterator = iterator.Current.SelectChildren(XPathNodeType.Element);

                while (trackIterator.MoveNext())
                {
                    if (trackIterator.Current.Value == string.Empty)
                        continue;

                    if (trackIterator.Current.Name == "title")
                        t.Title = trackIterator.Current.Value;

                    if (trackIterator.Current.Name == "id")
                        t.Id = trackIterator.Current.Value;

                    if (trackIterator.Current.Name == "artist")
                        t.Aritist = trackIterator.Current.Value;

                    if (trackIterator.Current.Name == "bitrate" )
                        t.Bitrate = trackIterator.Current.ValueAsInt;

                    if (trackIterator.Current.Name == "samplerate" )
                        t.Samplerate = trackIterator.Current.ValueAsInt;

                    if (trackIterator.Current.Name == "md5")
                        t.Md5 = trackIterator.Current.Value;

                    if (trackIterator.Current.Name == "audio_summary")
                    {
                        XPathNodeIterator audiosummaryIterator = trackIterator.Current.SelectChildren(XPathNodeType.Element);
                        while (audiosummaryIterator.MoveNext())
                        {
                            if (audiosummaryIterator.Current.Value == string.Empty)
                                continue;

                            if (audiosummaryIterator.Current.Name == "loudness" )
                                t.Loudness = audiosummaryIterator.Current.ValueAsDouble;

                            if (audiosummaryIterator.Current.Name == "energy" )
                                t.Energy = audiosummaryIterator.Current.ValueAsDouble;

                            if (audiosummaryIterator.Current.Name == "danceability" )
                                t.Danceability = audiosummaryIterator.Current.ValueAsDouble;

                            if (audiosummaryIterator.Current.Name == "tempo" )
                                t.Tempo = audiosummaryIterator.Current.ValueAsDouble;

                            if (audiosummaryIterator.Current.Name == "duration" )
                                t.Duration = audiosummaryIterator.Current.ValueAsDouble;

                            if (audiosummaryIterator.Current.Name == "key" )
                                t.Key = audiosummaryIterator.Current.ValueAsInt;

                            if (audiosummaryIterator.Current.Name == "mode" )
                                t.Mode = audiosummaryIterator.Current.ValueAsInt;

                            if (audiosummaryIterator.Current.Name == "time_signature" )
                                t.TimeSignature = audiosummaryIterator.Current.ValueAsInt;
                        }
                    }

                   

                }

               
            }
          


            return t;

        }
        #endregion

    }
}
