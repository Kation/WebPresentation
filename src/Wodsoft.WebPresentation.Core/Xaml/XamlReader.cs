using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyModel;
using System.Xaml;
using System.Windows.Markup;
using System.Xml;
using Wodsoft.Web.Data;

namespace Wodsoft.Web.Xaml
{
    public class XamlReader
    {
        public object Load(Stream stream)
        {
            MemoryStream memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            memoryStream.Position = 0;
            return Load(new XamlXmlReader(memoryStream, GetSchemaContext()));
        }

        public object Load(XmlReader reader)
        {
            return Load(new XamlXmlReader(reader, GetSchemaContext()));
        }

        public object Load(TextReader reader)
        {
            return Load(new XamlXmlReader(reader, GetSchemaContext()));
        }

        public object Load(string filename)
        {
            Stream stream = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            return Load(new XamlXmlReader(stream, GetSchemaContext()));
        }

        private static Assembly[] _Assemblies;

        private XamlSchemaContext GetSchemaContext()
        {
            if (_Assemblies == null)
            {
                _Assemblies = DependencyContext.Default.RuntimeLibraries.Select(s => s.Name)
                    .Select(t =>
                    {
                        try
                        {
                            return Assembly.Load(new AssemblyName(t));
                        }
                        catch (Exception)
                        {

                            return null;
                        }
                    }).Where(t => t != null).ToArray();
            }
            XamlSchemaContext schemaContext = new XamlSchemaContext(_Assemblies);
            return schemaContext;
        }

        private object Load(XamlXmlReader reader)
        {
            var context = new XamlSchemaContext();
            var setting = new XamlObjectWriterSettings();
            XamlObjectWriter writer = new ObjectWriter(context, setting);
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XamlNodeType.StartMember:
                        writer.WriteNode(reader);
                        //if (reader.Member.DeclaringType != null && typeof(DependencyObject).IsAssignableFrom(reader.Member.DeclaringType.UnderlyingType))
                        //{
                        //    //如果是属性
                        //    if (reader.Member.UnderlyingMember is PropertyInfo)
                        //    {
                        //        //获取依赖属性
                        //        DependencyProperty dp = DependencyProperty.FromName(reader.Member.Name, reader.Member.DeclaringType.UnderlyingType);
                        //        if (dp != null)
                        //        {
                        //            reader.Read();
                        //            if (reader.NodeType == XamlNodeType.StartObject && typeof(MarkupExtension).IsAssignableFrom(reader.Type.UnderlyingType))
                        //            {
                        //                int num = 1;
                        //                XamlObjectWriter innerWriter = new ObjectWriter(context, setting);
                        //                do
                        //                {
                        //                    innerWriter.WriteNode(reader);
                        //                    if (reader.NodeType == XamlNodeType.StartObject)
                        //                        num++;
                        //                    else if (reader.NodeType == XamlNodeType.EndObject)
                        //                        num--;
                        //                }
                        //                while (num > 0 && reader.Read());
                        //                writer.WriteValue(innerWriter.Result);
                        //            }
                        //            else
                        //            {
                        //                writer.WriteNode(reader);
                        //            }
                        //        }
                        //    }
                        //}
                        break;
                    default:
                        writer.WriteNode(reader);
                        break;
                }
            }
            //writer.Close();
            //reader.Close();
            if (writer.Result is DependencyObject && !(writer is INameScope))
                ((DependencyObject)writer.Result).SetValue(NameScope.NameScopeProperty, writer.RootNameScope);
            return writer.Result;
        }
    }
}
