using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
        private const string apiUrl = "http://api.forismatic.com/api/1.0/?method=getQuote&lang=ru&format=xml";
        private const string twitterShareUrl = "http://twitter.com/home?status=";
        private const string m_FacebookShareUrl = "http://www.facebook.com/sharer.php?u=";
        private const string m_VkontakteShareUrl = "http://vkontakte.ru/share.php?url=";

        private WebClient m_WebClient;
        private Quote m_Quote;

        public MainPage()
        {
            InitializeComponent();
            
            // Create WebClient and get first data
            m_WebClient = new WebClient();
            m_WebClient.BaseAddress = "http://api.forismatic.com/api/1.0/";
             
            m_WebClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(ReadAnswer);

            // Random to generate random key
            Random random = new Random();
            m_WebClient.DownloadStringAsync(new Uri(apiUrl + "&key=" + random.Next(999999) ));

            // Disable refresh button during get new data
            refreshButton.IsEnabled = false;

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
            Random random = new Random();
            m_WebClient.DownloadStringAsync(new Uri(apiUrl + "&key=" + random.Next(999999)));           
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

                // Set Twitter button Uri.
                string twitterUrl = String.Empty;
                if (quoteAuthor.Text != String.Empty)
                {
                    twitterUrl = String.Format("http://twitter.com/home?status={0}©{1}.{2}", HttpUtility.UrlEncode(quoteText.Text), HttpUtility.UrlEncode(quoteAuthor.Text), HttpUtility.UrlEncode("#forismatic") );
                    twitterUrl = twitterShareUrl + HttpUtility.UrlEncode(quoteText.Text + "©" + quoteAuthor.Text + " #forismatic"); 
                }
                else if (quoteText.Text != String.Empty)
                {
                    twitterUrl = twitterShareUrl + HttpUtility.UrlEncode(quoteText.Text + " #forismatic"); 
                }
                twitterButton.NavigateUri = new Uri(twitterUrl);

                // Set Facebook button Uri.
                string facebookUrl = String.Empty;
                if (m_Quote.Link != String.Empty)
                {
                    facebookUrl = m_FacebookShareUrl + m_Quote.Link;
                }
                facebookButton.NavigateUri = new Uri(facebookUrl);

                // Set Vkontakte button Uri
                string vkontakteUrl = String.Empty;
                if (m_Quote.Link != String.Empty)
                {
                    if (m_Quote.Author != String.Empty)
                    {
                        vkontakteUrl = m_VkontakteShareUrl + m_Quote.Link + "&title=" + HttpUtility.UrlEncode(m_Quote.Text) + "&description=" + HttpUtility.UrlEncode(m_Quote.Text + "©" + m_Quote.Author);
                    }
                    else
                    {
                        vkontakteUrl = m_VkontakteShareUrl + m_Quote.Link + "&title=" + HttpUtility.UrlEncode(m_Quote.Text) + "&description=" + HttpUtility.UrlEncode(m_Quote.Text);
                    }
                }
                vkontakteButton.NavigateUri = new Uri(vkontakteUrl);

                // Set Wiki button Uri.
                if (m_Quote.Author != String.Empty)
                {
                    wikiButton.NavigateUri = new Uri(String.Format("http://ru.wikipedia.org/wiki/{0}", quoteAuthor.Text));
                    wikiButton.IsEnabled = true;
                }
                else
                {
                    wikiButton.IsEnabled = false;
                }
            }     
            
            // Enable Refresh button
            refreshButton.IsEnabled = true;
        }
        
        private void facebookButton_Click(object sender, RoutedEventArgs e)
        {
          
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
