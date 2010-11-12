using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ElectricSheep.EchoNestTestApp
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ElectricSheep.EchoNestLib.APIs.ArtistAPI.Instance.Search("electric sheep");
            List<string> bu = new List<string>();

            bu.Add("news");
            bu.Add("reviews");

            EchoNestLib.APIs.ArtistAPI.Instance.Search("radiohead", 1, EchoNestLib.APIs.ArtistAPI.ArtistSortType.HotttnesssDesc, true, bu);
        }

        private void Track_click(object sender, RoutedEventArgs e)
        {
            ElectricSheep.EchoNestLib.APIs.TrackAPI.Instance.Upload("http://www.theelectricsheep.com/working/Beginnin-MozartFull.mp3");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //calling
            //EchoNestLib.APIs.TrackAPI.Instance.Analyze("TRMFRFJ12C1992D658", string.Empty); 

            //beginning
            ElectricSheep.EchoNestLib.APIs.TrackAPI.Instance.Analyze("TRBIIJN12C1992D6C8", string.Empty);
            

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ElectricSheep.EchoNestLib.APIs.ArtistAPI.Instance.Audio("beatles", string.Empty, 40, 23);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            double value = ElectricSheep.EchoNestLib.APIs.ArtistAPI.Instance.Familiarity("rolling stones", string.Empty);
        }

       


    }
}
