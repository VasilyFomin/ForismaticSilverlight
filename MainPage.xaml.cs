using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Browser;

namespace ForismaticGadget
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();         
        }

        private void LogoClicked(object sender, MouseButtonEventArgs e)
        {
            HtmlPage.Window.Navigate(new Uri("http://forismatic.com", UriKind.Absolute), "_blank");
        }

        private void RefreshClicked(object sender, RoutedEventArgs e)
        {
            WebClient webClient = new WebClient();
            string apiUrl = "http://api.forismatic.com/api/1.0/?method=getQuote&format=xml&lang=ru";

            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(ReadAnswer);
            webClient.DownloadStringAsync(new Uri(apiUrl));
        }

        private void ReadAnswer(Object sender, DownloadStringCompletedEventArgs e)
        {
            if ( !e.Cancelled && e.Error == null )
            {
               // string answer = e.Result.ToString();
                //MessageBox.Show(answer);
                

                Quote receivedQuote = Quote.Parse(e.Result.ToString());

                quoteText.Text = receivedQuote.Text;
                quoteAuthor.Text = receivedQuote.Author;
            }
        }
    }
}
