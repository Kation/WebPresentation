using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xaml;

namespace Wodsoft.WebPresentation.ConsoleTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.SetWindowSize(240, 60);
#if NET461
            Console.WriteLine(".Net 4.6.1");
#else
            Console.WriteLine(".Net Core App 1.1");
#endif
            List<Assembly> assemblies = new List<Assembly>()
            {
                Assembly.Load(new AssemblyName("Wodsoft.WebPresentation.Core")),
                Assembly.Load(new AssemblyName("Wodsoft.WebPresentation")),
                Assembly.Load(new AssemblyName("Wodsoft.WebPresentation.Visualization"))
            };
            XamlSchemaContext context = new XamlSchemaContext(assemblies);
            XamlXmlReader reader = new XamlXmlReader("Global.xaml", context);
            Console.WriteLine("Line".PadRight(10, ' ') + "NodeType".PadRight(30, ' ') + "Value");
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case System.Xaml.XamlNodeType.StartMember:
                        Console.WriteLine((reader.LineNumber + "," + reader.LinePosition).PadRight(10, ' ') + reader.NodeType.ToString().PadRight(30, ' ') + reader.Member.ToString());
                        break;
                    case System.Xaml.XamlNodeType.StartObject:
                        Console.WriteLine((reader.LineNumber + "," + reader.LinePosition).PadRight(10, ' ') + reader.NodeType.ToString().PadRight(30, ' ') + reader.Type.ToString());
                        break;
                    case System.Xaml.XamlNodeType.NamespaceDeclaration:
                        Console.WriteLine((reader.LineNumber + "," + reader.LinePosition).PadRight(10, ' ') + reader.NodeType.ToString().PadRight(30, ' ') + reader.Namespace.ToString());
                        break;
                    case System.Xaml.XamlNodeType.Value:
                        Console.WriteLine((reader.LineNumber + "," + reader.LinePosition).PadRight(10, ' ') + reader.NodeType.ToString().PadRight(30, ' ') + reader.Value.ToString());
                        break;
                    case System.Xaml.XamlNodeType.EndMember:
                    case System.Xaml.XamlNodeType.None:
                    case System.Xaml.XamlNodeType.EndObject:
                        Console.WriteLine((reader.LineNumber + "," + reader.LinePosition).PadRight(10, ' ') + reader.NodeType.ToString().PadRight(30, ' ') + "null");
                        break;
                }
            }
            Console.ReadLine();
        }
    }
}
