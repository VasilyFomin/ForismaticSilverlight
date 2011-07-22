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

namespace ForismaticGadget
{
	public class Quote 
	{	
		public String Text;
	
		public String Author;

		public String Link;
	
		public Quote () { }
	
		public Quote(String text, String author) 
		{
			Text = text;
			Author = author;
		}
	
		public static Quote Parse(String forsmaticResponseString)
		{
			Quote quote = new Quote();

			// Create an XmlReader
			using (XmlReader reader = XmlReader.Create(new StringReader(forsmaticResponseString)))
			{
                while ( reader.Read() )
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name)
                        {
                            case "quoteText":
                                reader.Read();
                                quote.Text = reader.Value;
                                break;
                            
                            case "quoteAuthor":
                                reader.Read();
                                quote.Author = reader.Value;
                                break;

                            case "quoteLink":
                                reader.Read();
                                quote.Link = reader.Value;
                                break;                            
                        }
                    }
                }               
			}
		
			return quote;
		}

		public override string ToString()
		{
			return String.Format("Text:{0}\nAuthor:{1}\nLink:{2}\n", Text, Author, Link);
		}
	}
}
