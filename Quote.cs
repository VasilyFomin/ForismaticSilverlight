using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Xml;
using System.IO;
using System.Xml.Linq;
using System.Linq;

namespace ForismaticGadget
{
	public class Quote 
	{
        public string Text { get; set; }

        public string Author { get; set; }

        public string Link { get; set; }
	
		public Quote () { }
	
		public Quote(String text, String author) 
		{
			Text = text;
			Author = author;
		}
	
		public static Quote Parse(String forsmaticResponseString)
		{
            XElement xml = XElement.Parse(forsmaticResponseString);

            Quote quote = (from n in xml.Descendants("quote")
                           select new Quote()
                           {
                               Text = n.Element("quoteText").Value,
                               Author = n.Element("quoteAuthor").Value,
                               Link = n.Element("quoteLink").Value,
                           }).FirstOrDefault();

            return quote;

		}

		public override string ToString()
		{
			return String.Format("Text:{0}\nAuthor:{1}\nLink:{2}\n", Text, Author, Link);
		}
	}
}
