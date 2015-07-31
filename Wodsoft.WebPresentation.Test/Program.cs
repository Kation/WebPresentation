using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Wodsoft.Web.Controls;

namespace Wodsoft.Web.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Xaml.XamlReader reader = new Xaml.XamlReader();
            Page page = (Page)reader.Load("Test.xaml");
            Console.WriteLine(page.Title);
            var stream = File.Open("Test.html", FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
            StreamWriter writer = new StreamWriter(stream);
            page.Render(writer);
            //writer.Flush();
            //writer.Close();
            Console.ReadLine();

            //XmlWriterSettings setting = new XmlWriterSettings();
            //setting.Encoding = new UTF8Encoding(false);
            //XmlWriter xmlWriter = XmlWriter.Create(writer, setting);
            //xmlWriter.WriteStartDocument();
            //xmlWriter.WriteStartElement("html");
            //xmlWriter.WriteStartElement("span");
            //xmlWriter.WriteString("test");
            //xmlWriter.WriteEndElement();
            //xmlWriter.WriteEndElement();
            //xmlWriter.WriteEndDocument();
            //xmlWriter.Flush();
            //xmlWriter.Close();
            //Console.ReadLine();
        }
    }
}
