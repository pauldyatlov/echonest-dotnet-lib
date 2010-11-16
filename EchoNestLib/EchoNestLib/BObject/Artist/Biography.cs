using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EchoNestLib.BObject.Artist
{
    /// <summary>
    /// This class represent a biography particle of an artist
    /// </summary>
    public class Biography
    {
        

        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }
        private string _url;


        

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }
        private string _text;

        

        public string Site
        {
            get { return _site; }
            set { _site = value; }
        }
        private string _site;

        

        public string Licence
        {
            get { return _licence; }
            set { _licence = value; }
        }
        private string _licence;
    }
}
