using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using ElectricSheep.EchoNestLib.BObject.Artist;
using System.Xml;
using System.Xml.XPath;
using EchoNestLib.BObject.Artist;

namespace ElectricSheep.EchoNestLib.APIs
{
    public class ArtistAPI
    {
        #region Singleton
        static ArtistAPI instance = null;
        static readonly object padlock = new object();

        ArtistAPI()
        {
            ArtistSortTypeDesc.Add(ArtistSortType.FamiliarityAsc, "familiarity-asc");
            ArtistSortTypeDesc.Add(ArtistSortType.FamiliarityDesc, "familiarity-desc");
            ArtistSortTypeDesc.Add(ArtistSortType.HotttnesssAsc, "hotttnesss-asc");
            ArtistSortTypeDesc.Add(ArtistSortType.HotttnesssDesc, "hotttnesss-desc");
        }

        public static ArtistAPI Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ArtistAPI();
                    }
                    return instance;
                }
            }
        }
        #endregion



        private string ArtistWS = "http://developer.echonest.com/api/v4/artist/search?";
        private string AudioWS = "http://developer.echonest.com/api/v4/artist/audio?";
        private string FamiliarityWS = "http://developer.echonest.com/api/v4/artist/familiarity?";

        public enum ArtistSortType
        {
            FamiliarityAsc = 0,
            FamiliarityDesc,
            HotttnesssAsc,
            HotttnesssDesc
        }

        Dictionary<ArtistSortType, string> ArtistSortTypeDesc = new Dictionary<ArtistSortType, string>();

        #region Familiarity
        private double ParseFamiliarityXml(XPathNodeIterator iterator)
        {
            while (iterator.MoveNext())
            {
                XPathNodeIterator audioIterator = iterator.Current.SelectChildren(XPathNodeType.Element);

                while (audioIterator.MoveNext())
                {
                    if (audioIterator.Current.Value == string.Empty)
                        continue;

                    if (audioIterator.Current.Name == "familiarity")
                        return audioIterator.Current.ValueAsDouble;
                }
            }

            return 0;
        }


        public double Familiarity(string name, string artistId)
        {
            string query = String.Format("{0}api_key={1}&name={2}&format=xml",
               FamiliarityWS,
               SharedData.APIKey,
               name               
               );

            if (artistId != string.Empty)
                query += "&id=" + artistId;

            SharedData.PerformGetRequest(query);
            string xmlResult = SharedData.ReadWebRequestResult();
            XmlReader reader = XmlReader.Create(new StringReader(xmlResult));

            XPathDocument doc = new XPathDocument(reader);
            XPathNavigator nav = doc.CreateNavigator();

            XPathExpression expr;
            expr = nav.Compile("/response/artist");
            XPathNodeIterator iterator = nav.Select(expr);
            
            return this.ParseFamiliarityXml(iterator);
            
        }
        #endregion

        #region Audio
        /// <summary>
        /// This method parses an audio element xml iterator
        /// </summary>
        /// <param name="iterator"></param>
        /// <returns></returns>
        private List<Audio> ParseAudioXml(XPathNodeIterator iterator)
        {
            List<Audio> res = new List<Audio>();
            
            while (iterator.MoveNext())
            {
                Audio a = new Audio();

                XPathNodeIterator audioIterator = iterator.Current.SelectChildren(XPathNodeType.Element);

                while (audioIterator.MoveNext())
                {
                    if (audioIterator.Current.Value == string.Empty)
                        continue;

                    if (audioIterator.Current.Name == "artist")
                        a.Artist = audioIterator.Current.Value;

                    if (audioIterator.Current.Name == "title")
                        a.Title = audioIterator.Current.Value;

                    if (audioIterator.Current.Name == "url")
                        a.Url = audioIterator.Current.Value;

                    if (audioIterator.Current.Name == "date")
                        a.Date = audioIterator.Current.ValueAsDateTime;

                    if (audioIterator.Current.Name == "length")
                        a.Length = audioIterator.Current.ValueAsDouble;

                    if (audioIterator.Current.Name == "link")
                        a.Link = audioIterator.Current.Value;

                    if (audioIterator.Current.Name == "id")
                        a.Id = audioIterator.Current.Value;

                    if (audioIterator.Current.Name == "release")
                        a.Release = audioIterator.Current.Value;

                }

                res.Add(a);
            }

            return res;
        }

        /// <summary>
        /// Audio element main method
        /// </summary>
        /// <param name="name">Name of the artist</param>
        /// <param name="artistId">ID of the artist</param>
        /// <param name="results">Number of results to show</param>
        /// <param name="start"># of the first result to show</param>
        /// <returns>List of Audio elements</returns>
        public List<Audio> Audio(string name, string artistId, int results, int start)
        {
            string query = String.Format("{0}api_key={1}&name={2}&format=xml&start={3}",
               AudioWS,
               SharedData.APIKey,
               name,
               start
               );

            if (artistId != string.Empty)
                query += "&id=" + artistId;

            SharedData.PerformGetRequest(query);
            string xmlResult = SharedData.ReadWebRequestResult();
            XmlReader reader = XmlReader.Create(new StringReader(xmlResult));

            XPathDocument doc = new XPathDocument(reader);
            XPathNavigator nav = doc.CreateNavigator();

            XPathExpression expr;
            expr = nav.Compile("/response/audio/audio");
            XPathNodeIterator iterator = nav.Select(expr);

            return this.ParseAudioXml(iterator);
            
        }
        #endregion

        #region Search
        public List<Artist> Search(string name)
        {
            return Search(name, 15, ArtistSortType.HotttnesssDesc, true, null);
        }

        public List<Artist> Search(string name, int results)
        {
            return Search(name, results, ArtistSortType.HotttnesssDesc, true, null);
        }

        public List<Artist> Search(string name, int results, ArtistSortType sortType)
        {
            return Search(name, results, sortType, true, null);
        }

        public List<Artist> Search(string name, int results, ArtistSortType sortType, bool fuzzySearch)
        {
            return Search(name, results, sortType, fuzzySearch, null);
        }

        /// <summary>
        /// Search main method with all param
        /// </summary>
        /// <param name="name">Artist name</param>
        /// <param name="results">Number of results to return</param>
        /// <param name="sortType">Order method - See the enum </param>
        /// <param name="fuzzy">Use fuzzy logic to search</param>
        /// <param name="bucketRequest">Array with strings containing the bucket to request. Standard are:
        /// familiarity
        /// hotttnesss</param>
        /// <returns>List of artists objects</returns>
        public List<Artist> Search(string name, int results, ArtistSortType sortType, bool fuzzy, List<string> bucketRequest)
        {

            Dictionary<string, bool> bucket = new Dictionary<string, bool>();


            bucket.Add("audio", false);
            bucket.Add("biographies", false);
            bucket.Add("blogs", false);
            bucket.Add("familiarity", true);
            bucket.Add("hotttnesss", true);
            bucket.Add("images", false);
            bucket.Add("news", false);
            bucket.Add("reviews", false);
            bucket.Add("terms", false);
            bucket.Add("urls", false);
            bucket.Add("video", false);
            bucket.Add("id:CA1234123412341234", false);
            bucket.Add("id:musicbrainz", false);
            bucket.Add("id:playme", false);
            bucket.Add("id:7digital", false);

            //we turn on bucket request for specific key depending on what we got in the method call
            if (bucketRequest != null)
            {
                foreach (string bs in bucketRequest)
                {
                    if (bucket.ContainsKey(bs))
                        bucket[bs] = true;

                }

            }

            string query = String.Format("{0}api_key={1}&name={2}&fuzzy_match={3}&format=xml",
                ArtistWS,
                SharedData.APIKey,
                name,
                fuzzy.ToString().ToLower()
                );


            foreach (string s in bucket.Keys)
            {
                if (bucket[s])
                    query += string.Format("&bucket={0}", s);

            }

            query += string.Format("&sort={0}", ArtistSortTypeDesc[sortType]);

            if (results > 1000 || results < 0)
                throw new ArgumentOutOfRangeException("Error: results range is 0 < x < 1000");

            query += string.Format("&results={0}", results);


            SharedData.PerformGetRequest(query);
            string xmlResult = SharedData.ReadWebRequestResult();



            List<Artist> res = new List<Artist>();

            XmlReader reader = XmlReader.Create(new StringReader(xmlResult));

            XPathDocument doc = new XPathDocument(reader);
            XPathNavigator nav = doc.CreateNavigator();

            XPathExpression expr;
            expr = nav.Compile("/response/artists/artist");
            XPathNodeIterator iterator = nav.Select(expr);

            while (iterator.MoveNext())
            {
                Artist a = new Artist();

                XPathNodeIterator artistIterator = iterator.Current.SelectChildren(XPathNodeType.Element);

                while (artistIterator.MoveNext())
                {
                    if (artistIterator.Current.Value == string.Empty)
                        continue;

                    if (artistIterator.Current.Name == "name")
                        a.Name = artistIterator.Current.Value;

                    if (artistIterator.Current.Name == "id")
                        a.Id = artistIterator.Current.Value;

                    if (artistIterator.Current.Name == "familiarity")
                        a.Familiarity = artistIterator.Current.ValueAsDouble;

                    if (artistIterator.Current.Name == "hotttnesss")
                        a.Hotness = artistIterator.Current.ValueAsDouble;

                    if (artistIterator.Current.Name == "biographies")
                    {
                        XPathNodeIterator biographyIterator = artistIterator.Current.SelectChildren(XPathNodeType.Element);
                        while (biographyIterator.MoveNext())
                        {
                            if (biographyIterator.Current.Name == "biography")
                                a.Biographies.Add(biographyIterator.Current.Value);

                        }
                    }

                    if (artistIterator.Current.Name == "audio")
                    {
                        a.Audio.AddRange(this.ParseAudioXml(artistIterator.Current.SelectChildren(XPathNodeType.Element)));
                    }


                    if (artistIterator.Current.Name == "video")
                    {
                        XPathNodeIterator videoIterator = artistIterator.Current.SelectChildren(XPathNodeType.Element);
                        while (videoIterator.MoveNext())
                        {
                            if (videoIterator.Current.Name == "url")
                                a.Video.Add(videoIterator.Current.Value);

                        }
                    }

                    if (artistIterator.Current.Name == "images")
                    {
                        XPathNodeIterator imagesIterator = artistIterator.Current.SelectChildren(XPathNodeType.Element);
                        while (imagesIterator.MoveNext())
                        {
                            if (imagesIterator.Current.Name == "image")
                                a.Images.Add(imagesIterator.Current.Value);

                        }
                    }

                    if (artistIterator.Current.Name == "news")
                    {
                        XPathNodeIterator newsIterator = artistIterator.Current.SelectChildren(XPathNodeType.Element);
                        while (newsIterator.MoveNext())
                        {
                            if (newsIterator.Current.Name == "news")
                                a.News.Add(newsIterator.Current.Value);

                        }
                    }

                    if (artistIterator.Current.Name == "reviews")
                    {
                        XPathNodeIterator reviewsIterator = artistIterator.Current.SelectChildren(XPathNodeType.Element);
                        while (reviewsIterator.MoveNext())
                        {
                            if (reviewsIterator.Current.Name == "review")
                                a.Reviews.Add(reviewsIterator.Current.Value);

                        }
                    }

                }

                res.Add(a);
            }
            return res;

        }
        #endregion

    }
}
