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

namespace EchoNestTestApp
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
            EchoNestLib.APIs.ArtistAPI.Instance.Search("electric sheep");
            List<string> bu = new List<string>();

            bu.Add("news");
            bu.Add("reviews");

            EchoNestLib.APIs.ArtistAPI.Instance.Search("radiohead", 1, EchoNestLib.APIs.ArtistAPI.ArtistSortType.HotttnesssDesc, true, bu);
        }

        private void Track_click(object sender, RoutedEventArgs e)
        {
            EchoNestLib.APIs.TrackAPI.Instance.Upload(string.Empty);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            EchoNestLib.APIs.TrackAPI.Instance.Analyze("TRMFRFJ12C1992D658", string.Empty);

        }


    }
}
