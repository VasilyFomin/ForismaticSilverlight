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
        private const string apiUrl = "http://api.forismatic.com/api/1.0/?method=getQuote&format=xml&lang=ru";
        private const string twitterShareUrl = "http://twitter.com/home?status=";
        private const string m_FacebookShareUrl = "http://www.facebook.com/sharer.php?u=";

        private WebClient m_WebClient;
        private Quote m_Quote;

        public MainPage()
        {
            InitializeComponent();
            m_WebClient = new WebClient();
            m_WebClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(ReadAnswer);
            m_WebClient.DownloadStringAsync(new Uri(apiUrl));

            m_Quote = new Quote();
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
            string twitterUrl = String.Empty;
            if (quoteAuthor.Text != String.Empty)
            {
                twitterUrl = String.Format("http://twitter.com/home?status={0}©{1}.{2}", HttpUtility.UrlEncode(quoteText.Text), HttpUtility.UrlEncode(quoteAuthor.Text), HttpUtility.UrlEncode("#forismatic") );
                twitterUrl = twitterShareUrl + HttpUtility.UrlEncode(quoteText.Text + "©" + quoteAuthor.Text + " #forismatic"); 
                //HtmlPage.Window.Navigate(new Uri(twitterUrl), "_blank");                
            }
            else if (quoteText.Text != String.Empty)
            {
                twitterUrl = twitterShareUrl + HttpUtility.UrlEncode(quoteText.Text + " #forismatic"); 
                //HtmlPage.Window.Navigate(new Uri(twitterUrl), "_blank");    
            }
            HtmlPage.Window.Navigate(new Uri(twitterUrl), "_blank");    
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
            m_WebClient.DownloadStringAsync(new Uri(apiUrl));
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
                m_Quote = Quote.Parse(e.Result.ToString());

                quoteText.Text = m_Quote.Text;
                if (m_Quote.Author != String.Empty)
                {
                    quoteAuthor.Text = m_Quote.Author;
                }
            }                        
        }

        

        private void facebookButton_Click(object sender, RoutedEventArgs e)
        {
            string facebookUrl = String.Empty;
            if (m_Quote.Link != String.Empty)
            {
                facebookUrl = m_FacebookShareUrl + m_Quote.Link;
            }
            HtmlPage.Window.Navigate(new Uri(facebookUrl), "_blank");    
        }

        private void facebookButton_MouseEnter(object sender, MouseEventArgs e)
        {
            BitmapImage img = new BitmapImage(new Uri("/facebook_button_focused.png", UriKind.Relative));
            try
            {
                facebookImage.Source = img;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void facebookButton_MouseLeave(object sender, MouseEventArgs e)
        {
            BitmapImage img = new BitmapImage(new Uri("/facebook_button_normal.png", UriKind.Relative));
            try
            {
                facebookImage.Source = img;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void vkontakteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void vkontakteButton_MouseEnter(object sender, MouseEventArgs e)
        {
            BitmapImage img = new BitmapImage(new Uri("/vkontakte_button_focused.png", UriKind.Relative));
            try
            {
                vkontakteImage.Source = img;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void vkontakteButton_MouseLeave(object sender, MouseEventArgs e)
        {
            BitmapImage img = new BitmapImage(new Uri("/vkontakte_button_normal.png", UriKind.Relative));
            try
            {
                vkontakteImage.Source = img;
            }
            catch (Exception)
            {

                throw;
            }
        }      
    }
}
