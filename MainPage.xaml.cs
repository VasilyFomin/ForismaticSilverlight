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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Browser;

namespace ForismaticGadget
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            WebClient webClient = new WebClient();            

            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(ReadAnswer);
            webClient.DownloadStringAsync(new Uri(apiUrl));
        }                 
 
        private void LogoButton_Click(object sender, RoutedEventArgs e)
        {
            HtmlPage.Window.Navigate(new Uri("http://forismatic.com", UriKind.Absolute), "_blank");
        }

        private void twitterButton_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage img = new BitmapImage(new Uri("/twitter_button_clicked.png", UriKind.Relative));
            try
            {
                twitterImage.Source = img;
            }
            catch (Exception)
            {

                throw;
            }
            if (quoteAuthor.Text != String.Empty)
            {
                Uri twitterShareUri = new Uri(String.Format("http://twitter.com/home?status={0}©{1}.%23forismatic", quoteText.Text, quoteAuthor.Text));
                HtmlPage.Window.Navigate(twitterShareUri, "_blank");                
            }
            else if (quoteText.Text != String.Empty)
            {
                Uri twitterShareUri = new Uri(String.Format("http://twitter.com/home?status={0}%23forismatic", quoteText.Text));
                HtmlPage.Window.Navigate(twitterShareUri, "_blank");
            }
        }

        private void twitterButton_MouseEnter(object sender, MouseEventArgs e)
        {
            BitmapImage img = new BitmapImage(new Uri("/twitter_button_focused.png", UriKind.Relative));
            try
            {
                twitterImage.Source = img;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void twitterButton_MouseLeave(object sender, MouseEventArgs e)
        {
            BitmapImage img = new BitmapImage(new Uri("/twitter_button_normal.png", UriKind.Relative));
            try
            {
                twitterImage.Source = img;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void wikiButton_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage img = new BitmapImage(new Uri("/wiki_button_clicked.png", UriKind.Relative));
            try
            {
                wikiImage.Source = img;
            }
            catch (Exception)
            {

                throw;
            }

            if (quoteAuthor.Text != String.Empty)
            {
                Uri wikiUri = new Uri(String.Format("http://ru.wikipedia.org/wiki/{0}", quoteAuthor.Text));
                HtmlPage.Window.Navigate(wikiUri, "_blank");
            }
        }

        private void wikiButton_MouseEnter(object sender, MouseEventArgs e)
        {
            BitmapImage img = new BitmapImage(new Uri("/wiki_button_focused.png", UriKind.Relative));
            try
            {
                wikiImage.Source = img;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void wikiButton_MouseLeave(object sender, MouseEventArgs e)
        {
            BitmapImage img = new BitmapImage(new Uri("/wiki_button_normal.png", UriKind.Relative));
            try
            {
                wikiImage.Source = img;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void copyButton_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage img = new BitmapImage(new Uri("/copy_button_clicked.png", UriKind.Relative));
            try
            {
                copyImage.Source = img;
            }
            catch (Exception)
            {

                throw;
            }

            if (quoteAuthor.Text != String.Empty)
            {
                Clipboard.SetText(String.Format("{0}©{1}.", quoteText.Text, quoteAuthor.Text));
            }
            else if (quoteText.Text != String.Empty)
            {
                Clipboard.SetText(String.Format("{0}", quoteText.Text));
            }

        }

        private void copyButton_MouseEnter(object sender, MouseEventArgs e)
        {
            BitmapImage img = new BitmapImage(new Uri("/copy_button_focused.png", UriKind.Relative));
            try
            {
                copyImage.Source = img;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void copyButton_MouseLeave(object sender, MouseEventArgs e)
        {
            BitmapImage img = new BitmapImage(new Uri("/copy_button_normal.png", UriKind.Relative));
            try
            {
                copyImage.Source = img;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage img = new BitmapImage(new Uri("/refresh_button_clicked.png", UriKind.Relative));
            try
            {
                refreshImage.Source = img;
            }
            catch (Exception)
            {

                throw;
            }

            WebClient webClient = new WebClient();
           
            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(ReadAnswer);
            webClient.DownloadStringAsync(new Uri(apiUrl));
        }

        private void refreshButton_MouseEnter(object sender, MouseEventArgs e)
        {
            BitmapImage img = new BitmapImage(new Uri("/refresh_button_focused.png", UriKind.Relative));
            try
            {
                refreshImage.Source = img;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void refreshButton_MouseLeave(object sender, MouseEventArgs e)
        {
            BitmapImage img = new BitmapImage(new Uri("/refresh_button_normal.png", UriKind.Relative));
            try
            {
                refreshImage.Source = img;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void ReadAnswer(Object sender, DownloadStringCompletedEventArgs e)
        {
            if (!e.Cancelled && e.Error == null)
            {
                Quote receivedQuote = Quote.Parse(e.Result.ToString());

                quoteText.Text = receivedQuote.Text;
                quoteAuthor.Text = receivedQuote.Author;
            }
        }

        private const string apiUrl = "http://api.forismatic.com/api/1.0/?method=getQuote&format=xml&lang=ru";
        
       
    }
}
