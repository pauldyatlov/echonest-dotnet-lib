using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EchoNestLib.BObject
{
    /// <summary>
    /// This class represent a Track in the echonest idea. A track is slightly like a song, but wiht 
    /// less attributes, like artistId
    /// </summary>
    public class Track
    {
        #region Properties

        public string Aritist
        {
            get { return _aritist; }
            set { _aritist = value; }
        }
        private string _aritist;


        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        private string _title;

        

        public string Md5
        {
            get { return _md5; }
            set { _md5 = value; }
        }
        private string _md5;

       

        public int Bitrate
        {
            get { return _bitrate; }
            set { _bitrate = value; }
        }
        private int _bitrate;

        

        public int Samplerate
        {
            get { return _samplerate; }
            set { _samplerate = value; }
        }
        private int _samplerate;

        
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _id;

        

        public double Energy
        {
            get { return _energy; }
            set { _energy = value; }
        }
        private double _energy;

       

        public double Danceability
        {
            get { return _danceability; }
            set { _danceability = value; }
        }
        private double _danceability;

        

        public double Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }
        private double _duration;

        

        public int Key
        {
            get { return _key; }
            set { _key = value; }
        }
        private int _key;

        

        public double Tempo
        {
            get { return _tempo; }
            set { _tempo = value; }
        }
        private double _tempo;

        

        public int Mode
        {
            get { return _mode; }
            set { _mode = value; }
        }
        private int _mode;

        

        public int TimeSignature
        {
            get { return _timeSignature; }
            set { _timeSignature = value; }
        }
        private int _timeSignature;


        
        public double Loudness
        {
            get { return _loudness; }
            set { _loudness = value; }
        }
        private double _loudness;

        #endregion

    }
}
