using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EchoNestLib.BObject.Artist
{
    /// <summary>
    /// This class represent the blog particle of an artist
    /// </summary>
    public class Blog
    {
        

        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }
        private string _url;

        

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _name;

        

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _id;

        

        public string Summary
        {
            get { return _summary; }
            set { _summary = value; }
        }
        private string _summary;

        

        public DateTime DatePosted
        {
            get { return _datePosted; }
            set { _datePosted = value; }
        }
        private DateTime _datePosted;

        

        public DateTime DateFound
        {
            get { return _dateFound; }
            set { _dateFound = value; }
        }
        private DateTime _dateFound;
    }
}
