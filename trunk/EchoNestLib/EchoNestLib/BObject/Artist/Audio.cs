using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EchoNestLib.BObject.Artist
{
    /// <summary>
    /// This class represents an audio particle with is data an URL
    /// </summary>
    public class Audio
    {

        public string Artist
        {
            get { return _artist; }
            set { _artist = value; }
        }
        private string _artist;


        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        private string _title;


        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }
        private string _url;
     

        public double Length
        {
            get { return _length; }
            set { _length = value; }
        }
        private double _length;


        

        public string Link
        {
            get { return _link; }
            set { _link = value; }
        }
        private string _link;


        

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }
        private DateTime _date;


        
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _id;


       

        public string Release
        {
            get { return _release; }
            set { _release = value; }
        }
        private string _release;

    }

}
